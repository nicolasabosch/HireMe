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
    [Route("api/CompanyType")]
    [ApiController]
    public class CompanyTypeController : ControllerBase
    {
        private readonly Entities db;
        public CompanyTypeController(Entities context)
        {
            db = context;
        }

        // GET api/CompanyType
        [HttpGet]
        public ActionResult GetAll()
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var companyTypeTranslation = DataTranslationLib.GetTranslation<Model.CompanyType>(db, languageID);
            var list = (
                from CompanyType in db.CompanyType
                join companyType in companyTypeTranslation on CompanyType.CompanyTypeID equals companyType.ID
                select new
                {
                CompanyType.CompanyTypeID, CompanyTypeName = companyType.Name, }

            );
            if (parameters["key"] != null)
            {
                string key = parameters["key"];
                list = list.Where(l => l.CompanyTypeID == key);
            }

            if (parameters["CompanyTypeID"] != null)
            {
                string companyTypeID = parameters["CompanyTypeID"];
                list = list.Where(l => l.CompanyTypeID == companyTypeID);
            }

            if (parameters["CompanyTypeName"] != null)
            {
                string companyTypeName = parameters["CompanyTypeName"];
                list = list.Where(l => l.CompanyTypeName.Contains(companyTypeName));
                list = list.OrderBy(l => l.CompanyTypeName.IndexOf(companyTypeName));
            }

            var ret = list.AsEnumerable();
            return Ok(ret);
        }

        // GET api/CompanyType/5
        [HttpGet("{id}")]
        public ActionResult GetCompanyType(string id)
        {
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var companyType = (
                from CompanyType in db.CompanyType
                where CompanyType.CompanyTypeID == id
                select new
                {
                CompanyType.CompanyTypeID, CompanyType.CompanyTypeName, CompanyType.CreatedOn, CompanyType.CreatedBy, CompanyType.LastModifiedOn, CompanyType.LastModifiedBy
                }

            ).FirstOrDefault();
            if (companyType == null)
            {
                return NotFound();
            }

            dynamic record = companyType.ToExpando();
            return Ok(record);
        }

        // PUT api/CompanyType/5
        [HttpPut("{id}")]
        public ActionResult PutCompanyType(string id, CompanyType companyType)
        {
            ModelState.Clear();
            Extensions.ClearReferences(companyType);
            TryValidateModel(companyType);
            if (ModelState.IsValid && id == companyType.CompanyTypeID)
            {
                db.Entry(companyType).State = EntityState.Modified;
                Entities.ProcessChildrenUpdate(db, companyType.DataTranslation.ToList());
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

        // POST api/CompanyType
        [HttpPost]
        public ActionResult<CompanyType> PostCompanyType(CompanyType companyType)
        {
            ModelState.Clear();
            Extensions.ClearReferences(companyType);
            TryValidateModel(companyType);
            if (ModelState.IsValid)
            {
                db.CompanyType.Add(companyType);
                Entities.ProcessChildrenUpdate(db, companyType.DataTranslation.ToList());
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (CompanyTypeExists(companyType.CompanyTypeID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetCompanyType", new
                {
                id = companyType.CompanyTypeID
                }

                , companyType);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/CompanyType/5
        [HttpDelete("{id}")]
        public ActionResult<CompanyType> DeleteCompanyType(string id)
        {
            CompanyType companyType = db.CompanyType.Find(id);
            if (companyType == null)
            {
                return NotFound();
            }

            db.CompanyType.Remove(companyType);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return companyType;
        }

        private bool CompanyTypeExists(string id)
        {
            return db.CompanyType.Any(e => e.CompanyTypeID == id);
        }
    }
}