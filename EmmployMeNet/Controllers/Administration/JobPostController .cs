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

            var currentUserID = this.CurrentUserID();

            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            var previewList = new[] { ".jpeg", ".jpg", ".png", ".gif", ".bmp" };
            var list = (
                from JobPost in db.JobPost
                join User in db.User on JobPost.UserID equals User.UserID
                join JobCategory in db.JobCategory on JobPost.JobCategoryID equals JobCategory.JobCategoryID
                join JobPostStatus in db.JobPostStatus on JobPost.JobPostStatusID equals JobPostStatus.JobPostStatusID
                where JobPost.UserID == currentUserID
                select new
                {
                    JobPost.JobPostID,
                    JobPost.JobPostName,
                    JobPost.JobPostDate,
                    JobPost.UserID,
                    User.UserName,
                    JobPost.JobCategoryID,
                    JobCategory.JobCategoryName,
                    JobPost.JobPostStatusID,
                    JobPostStatus.JobPostStatusName,
                }

            ); ;
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

            if (parameters["JobPostStatusID"] != null)
            {
                string jobPostStatusID = parameters["JobPostStatusID"];
                list = list.Where(l => l.JobPostStatusID == jobPostStatusID);
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
                    JobPost.JobPostStatusID,
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

            var currentUserID = this.CurrentUserID();

            record.applicance = (
                from JobApplicance in db.JobApplicance
                join JobApplicanceStatus in db.JobApplicanceStatus on JobApplicance.JobApplicanceStatusID equals JobApplicanceStatus.JobApplicanceStatusID
                from File in db.File.Where(X => X.FileID == JobApplicance.JobApplicanceFileID).DefaultIfEmpty()
                where JobApplicance.JobPostID == id && JobApplicance.UserID == currentUserID
                select new
                {

                    JobApplicance.JobApplicanceID,
                    JobApplicance.UserID,
                    JobApplicance.JobApplicanceStatusID,
                    JobApplicanceStatus.JobApplicanceStatusName,
                    JobApplicance.JobApplicanceText,
                    JobApplicance.JobApplicanceFileID,
                    JobApplicance.CreatedOn,
                    File.FileName
                }).FirstOrDefault();


            record.JobApplicance = (
                from JobApplicance in db.JobApplicance
                join User in db.User on JobApplicance.UserID equals User.UserID
                from File in db.File.Where(X => X.FileID == JobApplicance.JobApplicanceFileID).DefaultIfEmpty()
                where JobApplicance.JobPostID == id 
                select new
                {

                    JobApplicance.JobApplicanceID,
                    JobApplicance.JobPostID,
                    JobApplicance.UserID,
                    User.UserName,
                    User.Email,
                    JobApplicance.JobApplicanceStatusID,
                    JobApplicance.JobApplicanceText,
                    JobApplicance.JobApplicanceFileID,
                    ApplicanceDate = JobApplicance.CreatedOn,
                    JobApplicance.LastModifiedOn,
                    File.FileName

                }).ToList();


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



        [HttpGet]
        [Route("OpenedJobPost")]
        public ActionResult OpenedJobPost()
        {

            var currentUserID = this.CurrentUserID();

            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            var previewList = new[] { ".jpeg", ".jpg", ".png", ".gif", ".bmp" };
            var list = (
                from JobPost in db.JobPost
                join User in db.User on JobPost.UserID equals User.UserID
                join JobCategory in db.JobCategory on JobPost.JobCategoryID equals JobCategory.JobCategoryID
                join JobPostStatus in db.JobPostStatus on JobPost.JobPostStatusID equals JobPostStatus.JobPostStatusID
                from JobApplicance in db.JobApplicance.Where(X => X.JobPostID == JobPost.JobPostID && X.UserID == currentUserID).DefaultIfEmpty()
                from JobApplicanceStatus in db.JobApplicanceStatus.Where(X => X.JobApplicanceStatusID == JobApplicance.JobApplicanceStatusID).DefaultIfEmpty()

                where JobPost.JobPostStatusID == "OPENED"
                select new
                {
                    JobPost.JobPostID,
                    JobPost.JobPostName,
                    JobPost.JobPostDate,
                    JobPost.UserID,
                    User.UserName,
                    JobPost.JobCategoryID,
                    JobCategory.JobCategoryName,
                    JobPost.JobPostStatusID,
                    JobPostStatus.JobPostStatusName,
                    JobApplicance.JobApplicanceID,
                    ApplicanceDate =  JobApplicance.CreatedOn,
                    JobApplicanceStatus.JobApplicanceStatusName

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

            if (parameters["JobPostStatusID"] != null)
            {
                string jobPostStatusID = parameters["JobPostStatusID"];
                list = list.Where(l => l.JobPostStatusID == jobPostStatusID);
            }

            var ret = list.AsEnumerable();
            return Ok(ret);
        }


        [HttpPost]
        [Route("ApplyJobPost")]
        public ActionResult<JobApplicance> ApplyJobPost(JobApplicance jobApplicance)
        {
            jobApplicance.UserID = this.CurrentUserID();
            jobApplicance.JobApplicanceStatusID = "PENDING";
            db.JobApplicance.Add(jobApplicance);
            db.SaveChanges();

            return CreatedAtAction("ApplyJobPost", new { id = jobApplicance.JobApplicanceID }, jobApplicance);
        }





        [HttpPost]
        [Route("UpdateJobAppicanceStatus")]
        public ActionResult<JobApplicance> UpdateJobAppicanceStatus(JobApplicance jobApplicance)
        {

            var ja = db.JobApplicance.Find(jobApplicance.JobApplicanceID);
            ja.JobApplicanceStatusID = jobApplicance.JobApplicanceStatusID;
            db.SaveChanges();

            return CreatedAtAction("ApplyJobPost", new { id = jobApplicance.JobApplicanceID }, jobApplicance);
        }

        [HttpGet]
        [Route("MyOpportunity")]
        public ActionResult MyOpportunity()
        {

            var currentUserID = this.CurrentUserID();

            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            var previewList = new[] { ".jpeg", ".jpg", ".png", ".gif", ".bmp" };
            var list = (
                from JobPost in db.JobPost
                join User in db.User on JobPost.UserID equals User.UserID
                join JobCategory in db.JobCategory on JobPost.JobCategoryID equals JobCategory.JobCategoryID
                join JobPostStatus in db.JobPostStatus on JobPost.JobPostStatusID equals JobPostStatus.JobPostStatusID
                join JobApplicance in db.JobApplicance on JobPost.JobPostID equals JobApplicance.JobPostID
                from JobApplicanceStatus in db.JobApplicanceStatus.Where(X => X.JobApplicanceStatusID == JobApplicance.JobApplicanceStatusID).DefaultIfEmpty()

                where JobApplicance.UserID == currentUserID
                select new
                {
                    JobPost.JobPostID,
                    JobPost.JobPostName,
                    JobPost.JobPostDate,
                    JobPost.UserID,
                    User.UserName,
                    JobPost.JobCategoryID,
                    JobCategory.JobCategoryName,
                    JobPost.JobPostStatusID,
                    JobPostStatus.JobPostStatusName,
                    JobApplicance.JobApplicanceID,
                    ApplicanceDate = JobApplicance.CreatedOn,
                    JobApplicanceStatus.JobApplicanceStatusName

                }

            );
            if (parameters["key"] != null)
            {
                int key = int.Parse(parameters["key"]);
                list = list.Where(l => l.JobPostID == key);
            }


            var ret = list.AsEnumerable();
            return Ok(ret);
        }



        private bool JobPostExists(int id)
        {
            return db.JobPost.Any(e => e.JobPostID == id);
        }
    }
}