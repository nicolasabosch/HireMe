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
    [Route("api/JobRequest")]
    [ApiController]
    public class JobRequestController : ControllerBase
    {
        private readonly Entities db;
        public JobRequestController(Entities context)
        {
            db = context;
        }

        // GET api/JobRequest
        [HttpGet]
        public ActionResult GetAll()
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var list = (
                from JobRequest in db.JobRequest
                join Company in db.Company on JobRequest.CompanyID equals Company.CompanyID
                join JobCategory in db.JobCategory on JobRequest.JobCategoryID equals JobCategory.JobCategoryID
                select new
                {
                JobRequest.JobRequestID, JobRequest.JobRequestName, JobRequest.CompanyID, Company.CompanyName, JobRequest.JobCategoryID, JobCategory.JobCategoryName, }

            );
            if (parameters["key"] != null)
            {
                int key = int.Parse(parameters["key"]);
                list = list.Where(l => l.JobRequestID == key);
            }

            if (parameters["JobRequestID"] != null)
            {
                int jobRequestID = int.Parse(parameters["JobRequestID"]);
                list = list.Where(l => l.JobRequestID == jobRequestID);
            }

            if (parameters["JobRequestName"] != null)
            {
                string jobRequestName = parameters["JobRequestName"];
                list = list.Where(l => l.JobRequestName.Contains(jobRequestName));
                list = list.OrderBy(l => l.JobRequestName.IndexOf(jobRequestName));
            }

            if (parameters["CompanyID"] != null)
            {
                int companyID = int.Parse(parameters["CompanyID"]);
                list = list.Where(l => l.CompanyID == companyID);
            }

            if (parameters["JobCategoryID"] != null)
            {
                string jobCategoryID = parameters["JobCategoryID"];
                list = list.Where(l => l.JobCategoryID == jobCategoryID);
            }

            var ret = list.AsEnumerable();
            return Ok(ret);
        }

        // GET api/JobRequest/5
        [HttpGet("{id}")]
        public ActionResult GetJobRequest(int id)
        {
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var jobRequest = (
                from JobRequest in db.JobRequest
                join Company in db.Company on JobRequest.CompanyID equals Company.CompanyID
                where JobRequest.JobRequestID == id
                select new
                {
                JobRequest.JobRequestID, JobRequest.JobRequestName, JobRequest.JobRequestDescription, JobRequest.CompanyID, Company = new
                {
                JobRequest.CompanyID, Company.CompanyName
                }

                , JobRequest.JobCategoryID, JobRequest.CreatedOn, JobRequest.CreatedBy, JobRequest.LastModifiedOn, JobRequest.LastModifiedBy
                }

            ).FirstOrDefault();
            if (jobRequest == null)
            {
                return NotFound();
            }

            dynamic record = jobRequest.ToExpando();
            record.JobRequestFile = (
                from JobRequestFile in db.JobRequestFile
                join File in db.File on JobRequestFile.FileID equals File.FileID
                where JobRequestFile.JobRequestID == id
                select new
                {
                JobRequestFile.JobRequestFileID, JobRequestFile.JobRequestFileName, JobRequestFile.JobRequestID, JobRequestFile.FileID, File.FileName, PreviewFileID = File == null ? false : previewList.Contains(File.FileName.ToLower().Substring(File.FileName.ToLower().IndexOf("."))), EntityStatus = "U", JobRequestFile.LastModifiedOn
                }

            ).ToList();
            record.JobRequestTool = (
                from JobRequestTool in db.JobRequestTool
                where JobRequestTool.JobRequestID == id
                select new
                {
                JobRequestTool.JobRequestID, JobRequestTool.ToolID, EntityStatus = "U", JobRequestTool.LastModifiedOn
                }

            ).ToList();
            record.JobRequestUser = (
                from JobRequestUser in db.JobRequestUser
                join User in db.User on JobRequestUser.UserID equals User.UserID
                where JobRequestUser.JobRequestID == id
                select new
                {
                JobRequestUser.JobRequestID, JobRequestUser.UserID, User.UserName, EntityStatus = "U", JobRequestUser.LastModifiedOn
                }

            ).ToList();
            return Ok(record);
        }

        // PUT api/JobRequest/5
        [HttpPut("{id}")]
        public ActionResult PutJobRequest(int id, JobRequest jobRequest)
        {
            ModelState.Clear();
            Extensions.ClearReferences(jobRequest);
            TryValidateModel(jobRequest);
            if (ModelState.IsValid && id == jobRequest.JobRequestID)
            {
                db.Entry(jobRequest).State = EntityState.Modified;
                Model.Entities.ProcessChildrenUpdate(db, jobRequest.JobRequestFile.ToList());
                Model.Entities.ProcessChildrenUpdate(db, jobRequest.JobRequestTool.ToList());
                Model.Entities.ProcessChildrenUpdate(db, jobRequest.JobRequestUser.ToList());
                Entities.ProcessChildrenUpdate(db, jobRequest.DataTranslation.ToList());
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

        // POST api/JobRequest
        [HttpPost]
        public ActionResult<JobRequest> PostJobRequest(JobRequest jobRequest)
        {
            ModelState.Clear();
            Extensions.ClearReferences(jobRequest);
            TryValidateModel(jobRequest);
            if (ModelState.IsValid)
            {
                db.JobRequest.Add(jobRequest);
                Entities.ProcessChildrenUpdate(db, jobRequest.DataTranslation.ToList());
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (JobRequestExists(jobRequest.JobRequestID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetJobRequest", new
                {
                id = jobRequest.JobRequestID
                }

                , jobRequest);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/JobRequest/5
        [HttpDelete("{id}")]
        public ActionResult<JobRequest> DeleteJobRequest(int id)
        {
            JobRequest jobRequest = db.JobRequest.Find(id);
            if (jobRequest == null)
            {
                return NotFound();
            }

            db.JobRequest.Remove(jobRequest);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return jobRequest;
        }

        private bool JobRequestExists(int id)
        {
            return db.JobRequest.Any(e => e.JobRequestID == id);
        }
    }
}