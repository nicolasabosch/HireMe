using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using System.Collections.Specialized;
using CabernetDBContext;
using EmmploymeNet.Model;

namespace EmmploymeNet.Controllers
{
    [Route("api/JobPostStatus")]
    [ApiController]
    public class JobPostStatusController : ControllerBase
    {
        private readonly Entities db;
        public JobPostStatusController(Entities context)
        {
            db = context;
        }

        // GET api/JobPostStatus
        [HttpGet]
        public ActionResult GetAll()
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var jobPostStatusTranslation = DataTranslationLib.GetTranslation<Model.JobPostStatus>(db, languageID);
            var list = (
                from JobPostStatus in db.JobPostStatus
                join jobPostStatus in jobPostStatusTranslation on JobPostStatus.JobPostStatusID equals jobPostStatus.ID
                select new
                {
                JobPostStatus.JobPostStatusID, JobPostStatusName = jobPostStatus.Name, }

            );
            if (parameters["key"] != null)
            {
                string key = parameters["key"];
                list = list.Where(l => l.JobPostStatusID == key);
            }

            if (parameters["JobPostStatusID"] != null)
            {
                string jobPostStatusID = parameters["JobPostStatusID"];
                list = list.Where(l => l.JobPostStatusID == jobPostStatusID);
            }

            if (parameters["JobPostStatusName"] != null)
            {
                string jobPostStatusName = parameters["JobPostStatusName"];
                list = list.Where(l => l.JobPostStatusName.Contains(jobPostStatusName));
                list = list.OrderBy(l => l.JobPostStatusName.IndexOf(jobPostStatusName));
            }

            var ret = list.AsEnumerable();
            return Ok(ret);
        }

        // GET api/JobPostStatus/5
        [HttpGet("{id}")]
        public ActionResult GetJobPostStatus(string id)
        {
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var jobPostStatus = (
                from JobPostStatus in db.JobPostStatus
                where JobPostStatus.JobPostStatusID == id
                select new
                {
                JobPostStatus.JobPostStatusID, JobPostStatus.JobPostStatusName, JobPostStatus.CreatedOn, JobPostStatus.CreatedBy, JobPostStatus.LastModifiedOn, JobPostStatus.LastModifiedBy
                }

            ).FirstOrDefault();
            if (jobPostStatus == null)
            {
                return NotFound();
            }

            dynamic record = jobPostStatus.ToExpando();
            return Ok(record);
        }

        // PUT api/JobPostStatus/5
        [HttpPut("{id}")]
        public ActionResult PutJobPostStatus(string id, JobPostStatus jobPostStatus)
        {
            ModelState.Clear();
            Extensions.ClearReferences(jobPostStatus);
            TryValidateModel(jobPostStatus);
            if (ModelState.IsValid && id == jobPostStatus.JobPostStatusID)
            {
                db.Entry(jobPostStatus).State = EntityState.Modified;
                Entities.ProcessChildrenUpdate(db, jobPostStatus.DataTranslation.ToList());
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST api/JobPostStatus
        [HttpPost]
        public ActionResult<JobPostStatus> PostJobPostStatus(JobPostStatus jobPostStatus)
        {
            ModelState.Clear();
            Extensions.ClearReferences(jobPostStatus);
            TryValidateModel(jobPostStatus);
            if (ModelState.IsValid)
            {
                db.JobPostStatus.Add(jobPostStatus);
                Entities.ProcessChildrenUpdate(db, jobPostStatus.DataTranslation.ToList());
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (JobPostStatusExists(jobPostStatus.JobPostStatusID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetJobPostStatus", new
                {
                id = jobPostStatus.JobPostStatusID
                }

                , jobPostStatus);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/JobPostStatus/5
        [HttpDelete("{id}")]
        public ActionResult<JobPostStatus> DeleteJobPostStatus(string id)
        {
            JobPostStatus jobPostStatus = db.JobPostStatus.Find(id);
            if (jobPostStatus == null)
            {
                return NotFound();
            }

            db.JobPostStatus.Remove(jobPostStatus);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return jobPostStatus;
        }

        private bool JobPostStatusExists(string id)
        {
            return db.JobPostStatus.Any(e => e.JobPostStatusID == id);
        }
    }
}