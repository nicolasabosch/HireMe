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
    [Route("api/Company")]
    [ApiController]
    [AllowAnonymous]
    public class CompanyController : ControllerBase
    {
        private readonly Entities db;
        public CompanyController(Entities context)
        {
            db = context;
        }

        // GET api/Company
        [HttpGet]
        public ActionResult GetAll()
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            var previewList = new[] { ".jpeg", ".jpg", ".png", ".gif", ".bmp" };
            var list = (
                from Company in db.Company
                join CompanyType in db.CompanyType on Company.CompanyTypeID equals CompanyType.CompanyTypeID
                select new
                {
                    Company.CompanyID,
                    Company.CompanyName,
                    CompanyFullName = Company.CompanyName + " (" + Company.CompanyID.ToString() + ")",
                    Company.CompanyTypeID,
                    CompanyType.CompanyTypeName,
                }

            );
            if (parameters["key"] != null)
            {
                int key = int.Parse(parameters["key"]);
                list = list.Where(l => l.CompanyID == key);
            }

            if (parameters["CompanyID"] != null)
            {
                int companyID = int.Parse(parameters["CompanyID"]);
                list = list.Where(l => l.CompanyID == companyID);
            }

            if (parameters["CompanyName"] != null)
            {
                string companyName = parameters["CompanyName"];
                list = list.Where(l => l.CompanyName.Contains(companyName));
                list = list.OrderBy(l => l.CompanyName.IndexOf(companyName));
            }


            if (parameters["CompanyFullName"] != null)
            {
                string companyName = parameters["CompanyFullName"];
                list = list.Where(l => l.CompanyFullName.Contains(companyName));
                list = list.OrderBy(l => l.CompanyFullName.IndexOf(companyName));
            }
            if (parameters["CompanyTypeID"] != null)
            {
                string companyTypeID = parameters["CompanyTypeID"];
                list = list.Where(l => l.CompanyTypeID == companyTypeID);
            }

            var ret = list.AsEnumerable();
            return Ok(ret);
        }

        // GET api/Company/5
        [HttpGet("{id}")]
        public ActionResult GetCompany(int id)
        {
            var languageID = this.CurrentLanguageID();
            var previewList = new[] { ".jpeg", ".jpg", ".png", ".gif", ".bmp" };
            var company = (
                from Company in db.Company
                where Company.CompanyID == id
                select new
                {
                    Company.CompanyID,
                    Company.CompanyName,
                    Company.CompanyTypeID,
                    Company.CreatedOn,
                    Company.CreatedBy,
                    Company.LastModifiedOn,
                    Company.LastModifiedBy
                }

            ).FirstOrDefault();
            if (company == null)
            {
                return NotFound();
            }

            dynamic record = company.ToExpando();
            return Ok(record);
        }

        // PUT api/Company/5
        [HttpPut("{id}")]
        public ActionResult PutCompany(int id, Company company)
        {
            ModelState.Clear();
            Extensions.ClearReferences(company);
            TryValidateModel(company);
            if (ModelState.IsValid && id == company.CompanyID)
            {
                db.Entry(company).State = EntityState.Modified;
                Entities.ProcessChildrenUpdate(db, company.DataTranslation.ToList());
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

        // POST api/Company
        [HttpPost]
        public ActionResult<Company> PostCompany(Company company)
        {
            ModelState.Clear();
            Extensions.ClearReferences(company);
            TryValidateModel(company);
            if (ModelState.IsValid)
            {
                db.Company.Add(company);
                Entities.ProcessChildrenUpdate(db, company.DataTranslation.ToList());
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (CompanyExists(company.CompanyID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetCompany", new
                {
                    id = company.CompanyID
                }

                , company);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/Company/5
        [HttpDelete("{id}")]
        public ActionResult<Company> DeleteCompany(int id)
        {
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            db.Company.Remove(company);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return company;
        }

        private bool CompanyExists(int id)
        {
            return db.Company.Any(e => e.CompanyID == id);
        }
    }
}