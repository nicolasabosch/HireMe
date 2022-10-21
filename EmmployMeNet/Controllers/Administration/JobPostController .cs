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
    [Route("api/JobPost")]
    [ApiController]
    public class JobPostController : ControllerBase
    {
        private readonly Entities db;
        public JobPostController(Entities context)
        {
            db = context;
        }

        // GET api/JobPost?param1=dato1&param2=dato2
        [HttpGet]
        public ActionResult GetAll()
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            var previewList = new[] { ".jpeg", ".jpg", ".png", ".gif", ".bmp" };
            var list = (
                from JobPost in db.JobPost
                join User in db.User on JobPost.UserID equals User.UserID
                join JobCategory in db.JobCategory on JobPost.JobCategoryID equals JobCategory.JobCategoryID
                select new
                {
                    JobPost.JobPostID,
                    JobPost.JobPostName,
                    JobPost.JobPostDate,
                    JobPost.UserID,
                    User.UserName,
                    JobPost.JobCategoryID,
                    JobCategory.JobCategoryName,
                }

            );
            if (parameters["key"] != null)
            {
                int key = int.Parse(parameters["key"]);
                list = list.Where(l => l.JobPostID == key);
            }

            if (parameters["JobPostID"] != null)
            {
                int jobPostID = int.Parse(parameters["JobPostID"]);
                list = list.Where(l => l.JobPostID == jobPostID);
            }

            if (parameters["JobPostName"] != null)
            {
                string jobPostName = parameters["JobPostName"];
                list = list.Where(l => l.JobPostName.Contains(jobPostName));
                list = list.OrderBy(l => l.JobPostName.IndexOf(jobPostName));
            }

            if (parameters["UserID"] != null)
            {
                string userID = parameters["UserID"];
                list = list.Where(l => l.UserID == userID);
            }

            if (parameters["JobCategoryID"] != null)
            {
                string jobCategoryID = parameters["JobCategoryID"];
                list = list.Where(l => l.JobCategoryID == jobCategoryID);
            }

            var ret = list.AsEnumerable();
            return Ok(ret);
        }

        // GET api/JobPost/5
        [HttpGet("{id}")]
        public ActionResult GetJobPost(int id)
        {
            var languageID = this.CurrentLanguageID();
            var previewList = new[] { ".jpeg", ".jpg", ".png", ".gif", ".bmp" };
            var jobPost = (
                from JobPost in db.JobPost
                join User in db.User on JobPost.UserID equals User.UserID
                where JobPost.JobPostID == id
                select new
                {
                    JobPost.JobPostID,
                    JobPost.JobPostName,
                    JobPost.JobPostDescription,
                    JobPost.JobPostDate,
                    JobPost.UserID,
                    User = new
                    {
                        JobPost.UserID,
                        User.UserName
                    }

                ,
                    JobPost.JobCategoryID,
                    JobPost.CreatedOn,
                    JobPost.CreatedBy,
                    JobPost.LastModifiedOn,
                    JobPost.LastModifiedBy
                }

            ).FirstOrDefault();
            if (jobPost == null)
            {
                return NotFound();
            }

            dynamic record = jobPost.ToExpando();

            record.JobPostSkill = (
                from JobPostSkill in db.JobPostSkill
                where JobPostSkill.JobPostID == id
                select new
                {
                    JobPostSkill.JobPostID,
                    JobPostSkill.JobCategorySkillID,
                    EntityStatus = "U",
                    JobPostSkill.LastModifiedOn
                }

            ).ToList();

            /*
            record.JobPostFile = (
                from JobPostFile in db.JobPostFile
                join File in db.File on JobPostFile.FileID equals File.FileID
                where JobPostFile.JobPostID == id
                select new
                {
                JobPostFile.JobPostFileID, JobPostFile.JobPostFileName, JobPostFile.JobPostID, JobPostFile.FileID, File.FileName, PreviewFileID = File == null ? false : previewList.Contains(File.FileName.ToLower().Substring(File.FileName.ToLower().IndexOf("."))), EntityStatus = "U", JobPostFile.LastModifiedOn
                }

            ).ToList();
            record.JobPostTool = (
                from JobPostTool in db.JobPostTool
                where JobPostTool.JobPostID == id
                select new
                {
                JobPostTool.JobPostID, JobPostTool.ToolID, EntityStatus = "U", JobPostTool.LastModifiedOn
                }

            ).ToList();
            record.JobPostUser = (
                from JobPostUser in db.JobPostUser
                join User in db.User on JobPostUser.UserID equals User.UserID
                where JobPostUser.JobPostID == id
                select new
                {
                JobPostUser.JobPostID, JobPostUser.UserID, User.UserName, EntityStatus = "U", JobPostUser.LastModifiedOn
                }

            ).ToList();
            */
            return Ok(record);
        }

        // PUT api/JobPost/5
        [HttpPut("{id}")]
        public ActionResult PutJobPost(int id, JobPost jobPost)
        {
            ModelState.Clear();
            Extensions.ClearReferences(jobPost);
            TryValidateModel(jobPost);
            if (ModelState.IsValid && id == jobPost.JobPostID)
            {
                db.Entry(jobPost).State = EntityState.Modified;
                //Model.Entities.ProcessChildrenUpdate(db, jobPost.JobPostFile.ToList());
                Model.Entities.ProcessChildrenUpdate(db, jobPost.JobPostSkill.ToList());
                //Model.Entities.ProcessChildrenUpdate(db, jobPost.JobPostUser.ToList());
                Entities.ProcessChildrenUpdate(db, jobPost.DataTranslation.ToList());
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

        // POST api/JobPost
        [HttpPost]
        public ActionResult<JobPost> PostJobPost(JobPost jobPost)
        {
            jobPost.UserID = this.CurrentUserID();
            ModelState.Clear();
            Extensions.ClearReferences(jobPost);
            TryValidateModel(jobPost);
            if (ModelState.IsValid)
            {
                db.JobPost.Add(jobPost);
                Entities.ProcessChildrenUpdate(db, jobPost.DataTranslation.ToList());
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (JobPostExists(jobPost.JobPostID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetJobPost", new
                {
                    id = jobPost.JobPostID
                }

                , jobPost);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/JobPost/5
        [HttpDelete("{id}")]
        public ActionResult<JobPost> DeleteJobPost(int id)
        {
            JobPost jobPost = db.JobPost.Find(id);
            if (jobPost == null)
            {
                return NotFound();
            }

            db.JobPost.Remove(jobPost);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return jobPost;
        }

        private bool JobPostExists(int id)
        {
            return db.JobPost.Any(e => e.JobPostID == id);
        }
    }
}