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
    [Route("api/JobCategory")]
    [ApiController]
    public class JobCategoryController : ControllerBase
    {
        private readonly Entities db;
        public JobCategoryController(Entities context)
        {
            db = context;
        }

        // GET api/JobCategory
        [HttpGet]
        public ActionResult GetAll()
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var list = (
                from JobCategory in db.JobCategory
                select new
                {
                JobCategory.JobCategoryID, JobCategory.JobCategoryName, }

            );
            if (parameters["key"] != null)
            {
                string key = parameters["key"];
                list = list.Where(l => l.JobCategoryID == key);
            }

            if (parameters["JobCategoryID"] != null)
            {
                string jobCategoryID = parameters["JobCategoryID"];
                list = list.Where(l => l.JobCategoryID == jobCategoryID);
            }

            if (parameters["JobCategoryName"] != null)
            {
                string jobCategoryName = parameters["JobCategoryName"];
                list = list.Where(l => l.JobCategoryName.Contains(jobCategoryName));
                list = list.OrderBy(l => l.JobCategoryName.IndexOf(jobCategoryName));
            }

            var ret = list.AsEnumerable();
            return Ok(ret);
        }

        // GET api/JobCategory/5
        [HttpGet("{id}")]
        public ActionResult GetJobCategory(string id)
        {
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var jobCategory = (
                from JobCategory in db.JobCategory
                where JobCategory.JobCategoryID == id
                select new
                {
                JobCategory.JobCategoryID, JobCategory.JobCategoryName, JobCategory.CreatedOn, JobCategory.CreatedBy, JobCategory.LastModifiedOn, JobCategory.LastModifiedBy
                }

            ).FirstOrDefault();
            if (jobCategory == null)
            {
                return NotFound();
            }

            dynamic record = jobCategory.ToExpando();
            record.JobCategorySkill = (
                from JobCategorySkill in db.JobCategorySkill
                where JobCategorySkill.JobCategoryID == id
                select new
                {
                JobCategorySkill.JobCategorySkillID, JobCategorySkill.JobCategorySkillName, JobCategorySkill.JobCategoryID, EntityStatus = "U", JobCategorySkill.LastModifiedOn
                }

            ).ToList();
            return Ok(record);
        }

        // PUT api/JobCategory/5
        [HttpPut("{id}")]
        public ActionResult PutJobCategory(string id, JobCategory jobCategory)
        {
            ModelState.Clear();
            Extensions.ClearReferences(jobCategory);
            TryValidateModel(jobCategory);
            if (ModelState.IsValid && id == jobCategory.JobCategoryID)
            {
                db.Entry(jobCategory).State = EntityState.Modified;
                Entities.ProcessChildrenUpdate(db, jobCategory.JobCategorySkill.ToList());
                Entities.ProcessChildrenUpdate(db, jobCategory.DataTranslation.ToList());
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

        // POST api/JobCategory
        [HttpPost]
        public ActionResult<JobCategory> PostJobCategory(JobCategory jobCategory)
        {
            ModelState.Clear();
            Extensions.ClearReferences(jobCategory);
            TryValidateModel(jobCategory);
            if (ModelState.IsValid)
            {
                db.JobCategory.Add(jobCategory);
                Entities.ProcessChildrenUpdate(db, jobCategory.DataTranslation.ToList());
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (JobCategoryExists(jobCategory.JobCategoryID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetJobCategory", new
                {
                id = jobCategory.JobCategoryID
                }

                , jobCategory);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/JobCategory/5
        [HttpDelete("{id}")]
        public ActionResult<JobCategory> DeleteJobCategory(string id)
        {
            JobCategory jobCategory = db.JobCategory.Find(id);
            if (jobCategory == null)
            {
                return NotFound();
            }

            db.JobCategory.Remove(jobCategory);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return jobCategory;
        }

        private bool JobCategoryExists(string id)
        {
            return db.JobCategory.Any(e => e.JobCategoryID == id);
        }
    }
}