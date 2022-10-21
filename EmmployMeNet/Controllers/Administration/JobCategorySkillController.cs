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
using Microsoft.AspNetCore.Authorization;

namespace EmmploymeNet.Controllers
{
    [Route("api/JobCategorySkill")]
    [ApiController]
    public class JobCategorySkillController : ControllerBase
    {
        private readonly Entities db;
        public JobCategorySkillController(Entities context)
        {
            db = context;
        }

        // GET api/JobCategorySkill
        [HttpGet]

        public ActionResult GetAll()
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();

            var list = (
                from JobCategorySkill in db.JobCategorySkill
                select new
                {
                    JobCategorySkill.JobCategoryID,
                    JobCategorySkill.JobCategorySkillID,
                    JobCategorySkill.JobCategorySkillName,
                }

            );

            var ret = list.AsEnumerable();
            return Ok(ret);
        }


    }
}