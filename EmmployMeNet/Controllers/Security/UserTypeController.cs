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
    [Route("api/UserType")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly Entities db;
        public UserTypeController(Entities context)
        {
            db = context;
        }

        // GET api/UserType
        [HttpGet]
        public ActionResult GetAll()
        {

            var x = new UserController(this.db );


            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var list = (
                from UserType in db.UserType
                select new
                {
                UserType.UserTypeID, UserType.UserTypeName, }

            );
            if (parameters["key"] != null)
            {
                string key = parameters["key"];
                list = list.Where(l => l.UserTypeID == key);
            }

            if (parameters["UserTypeID"] != null)
            {
                string userTypeID = parameters["UserTypeID"];
                list = list.Where(l => l.UserTypeID == userTypeID);
            }

            if (parameters["UserTypeName"] != null)
            {
                string userTypeName = parameters["UserTypeName"];
                list = list.Where(l => l.UserTypeName.Contains(userTypeName));
                list = list.OrderBy(l => l.UserTypeName.IndexOf(userTypeName));
            }

            var ret = list.AsEnumerable();
            return Ok(ret);
        }

        // GET api/UserType/5
        [HttpGet("{id}")]
        public ActionResult GetUserType(string id)
        {
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var userType = (
                from UserType in db.UserType
                where UserType.UserTypeID == id
                select new
                {
                UserType.UserTypeID, UserType.UserTypeName, UserType.CreatedOn, UserType.CreatedBy, UserType.LastModifiedOn, UserType.LastModifiedBy
                }

            ).FirstOrDefault();
            if (userType == null)
            {
                return NotFound();
            }

            dynamic record = userType.ToExpando();
            return Ok(record);
        }

        // PUT api/UserType/5
        [HttpPut("{id}")]
        public ActionResult PutUserType(string id, UserType userType)
        {
            ModelState.Clear();
            Extensions.ClearReferences(userType);
            TryValidateModel(userType);
            if (ModelState.IsValid && id == userType.UserTypeID)
            {
                db.Entry(userType).State = EntityState.Modified;
                Entities.ProcessChildrenUpdate(db, userType.DataTranslation.ToList());
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

        // POST api/UserType
        [HttpPost]
        public ActionResult<UserType> PostUserType(UserType userType)
        {
            ModelState.Clear();
            Extensions.ClearReferences(userType);
            TryValidateModel(userType);
            if (ModelState.IsValid)
            {
                db.UserType.Add(userType);
                Entities.ProcessChildrenUpdate(db, userType.DataTranslation.ToList());
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (UserTypeExists(userType.UserTypeID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetUserType", new
                {
                id = userType.UserTypeID
                }

                , userType);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/UserType/5
        [HttpDelete("{id}")]
        public ActionResult<UserType> DeleteUserType(string id)
        {
            UserType userType = db.UserType.Find(id);
            if (userType == null)
            {
                return NotFound();
            }

            db.UserType.Remove(userType);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return userType;
        }

        private bool UserTypeExists(string id)
        {
            return db.UserType.Any(e => e.UserTypeID == id);
        }
    }
}