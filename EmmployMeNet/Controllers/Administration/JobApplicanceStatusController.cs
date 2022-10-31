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
    [Route("api/JobApplicanceStatus")]
    [ApiController]
    public class JobApplicanceStatusController : ControllerBase
    {
        private readonly Entities db;
        public JobApplicanceStatusController(Entities context)
        {
            db = context;
        }

        // GET api/JobApplicanceStatus
        [HttpGet]
        public ActionResult GetAll()
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var jobApplicanceStatusTranslation = DataTranslationLib.GetTranslation<Model.JobApplicanceStatus>(db, languageID);
            var list = (
                from JobApplicanceStatus in db.JobApplicanceStatus
                join jobApplicanceStatus in jobApplicanceStatusTranslation on JobApplicanceStatus.JobApplicanceStatusID equals jobApplicanceStatus.ID
                select new
                {
                JobApplicanceStatus.JobApplicanceStatusID, JobApplicanceStatusName = jobApplicanceStatus.Name, }

            );
            if (parameters["key"] != null)
            {
                string key = parameters["key"];
                list = list.Where(l => l.JobApplicanceStatusID == key);
            }

            if (parameters["JobApplicanceStatusID"] != null)
            {
                string jobApplicanceStatusID = parameters["JobApplicanceStatusID"];
                list = list.Where(l => l.JobApplicanceStatusID == jobApplicanceStatusID);
            }

            if (parameters["JobApplicanceStatusName"] != null)
            {
                string jobApplicanceStatusName = parameters["JobApplicanceStatusName"];
                list = list.Where(l => l.JobApplicanceStatusName.Contains(jobApplicanceStatusName));
                list = list.OrderBy(l => l.JobApplicanceStatusName.IndexOf(jobApplicanceStatusName));
            }

            var ret = list.AsEnumerable();
            return Ok(ret);
        }

        // GET api/JobApplicanceStatus/5
        [HttpGet("{id}")]
        public ActionResult GetJobApplicanceStatus(string id)
        {
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var jobApplicanceStatus = (
                from JobApplicanceStatus in db.JobApplicanceStatus
                where JobApplicanceStatus.JobApplicanceStatusID == id
                select new
                {
                JobApplicanceStatus.JobApplicanceStatusID, JobApplicanceStatus.JobApplicanceStatusName, JobApplicanceStatus.CreatedOn, JobApplicanceStatus.CreatedBy, JobApplicanceStatus.LastModifiedOn, JobApplicanceStatus.LastModifiedBy
                }

            ).FirstOrDefault();
            if (jobApplicanceStatus == null)
            {
                return NotFound();
            }

            dynamic record = jobApplicanceStatus.ToExpando();
            return Ok(record);
        }

        // PUT api/JobApplicanceStatus/5
        [HttpPut("{id}")]
        public ActionResult PutJobApplicanceStatus(string id, JobApplicanceStatus jobApplicanceStatus)
        {
            ModelState.Clear();
            Extensions.ClearReferences(jobApplicanceStatus);
            TryValidateModel(jobApplicanceStatus);
            if (ModelState.IsValid && id == jobApplicanceStatus.JobApplicanceStatusID)
            {
                db.Entry(jobApplicanceStatus).State = EntityState.Modified;
                Entities.ProcessChildrenUpdate(db, jobApplicanceStatus.DataTranslation.ToList());
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

        // POST api/JobApplicanceStatus
        [HttpPost]
        public ActionResult<JobApplicanceStatus> PostJobApplicanceStatus(JobApplicanceStatus jobApplicanceStatus)
        {
            ModelState.Clear();
            Extensions.ClearReferences(jobApplicanceStatus);
            TryValidateModel(jobApplicanceStatus);
            if (ModelState.IsValid)
            {
                db.JobApplicanceStatus.Add(jobApplicanceStatus);
                Entities.ProcessChildrenUpdate(db, jobApplicanceStatus.DataTranslation.ToList());
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (JobApplicanceStatusExists(jobApplicanceStatus.JobApplicanceStatusID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetJobApplicanceStatus", new
                {
                id = jobApplicanceStatus.JobApplicanceStatusID
                }

                , jobApplicanceStatus);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/JobApplicanceStatus/5
        [HttpDelete("{id}")]
        public ActionResult<JobApplicanceStatus> DeleteJobApplicanceStatus(string id)
        {
            JobApplicanceStatus jobApplicanceStatus = db.JobApplicanceStatus.Find(id);
            if (jobApplicanceStatus == null)
            {
                return NotFound();
            }

            db.JobApplicanceStatus.Remove(jobApplicanceStatus);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return jobApplicanceStatus;
        }

        private bool JobApplicanceStatusExists(string id)
        {
            return db.JobApplicanceStatus.Any(e => e.JobApplicanceStatusID == id);
        }
    }
}