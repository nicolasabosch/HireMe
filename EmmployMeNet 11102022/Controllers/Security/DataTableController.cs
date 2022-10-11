using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using System.Collections.Specialized;
using EmmploymeNet.Model;
using CabernetDBContext;

namespace EmmploymeNet.Controllers
{
    [Route("api/DataTable")]
    [ApiController]
    public class DataTableController : ControllerBase
    {
        private readonly Entities db;
        public DataTableController(Entities context)
        {
            db = context;
        }

        // GET api/DataTable
        [HttpGet]
        public ActionResult GetAll()
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var dataTableTranslation = DataTranslationLib.GetTranslation<Model.DataTable>(db, languageID);
            var list = (
                from DataTable in db.DataTable
                join dataTable in dataTableTranslation on DataTable.DataTableID equals dataTable.ID
                select new
                {
                DataTable.DataTableID, DataTableName = dataTable.Name, }

            );
            if (parameters["key"] != null)
            {
                string key = parameters["key"];
                list = list.Where(l => l.DataTableID == key);
            }

            if (parameters["DataTableID"] != null)
            {
                string dataTableID = parameters["DataTableID"];
                list = list.Where(l => l.DataTableID == dataTableID);
            }

            if (parameters["DataTableName"] != null && parameters["DataTableFullName"] == null)
            {
                string dataTableName = parameters["DataTableName"];
                list = list.Where(l => l.DataTableName.Contains(dataTableName));
                list = list.OrderBy(l => l.DataTableName.IndexOf(dataTableName));
            }

            var ret = list.AsEnumerable();
            return Ok(ret);
        }

        // GET api/DataTable/5
        [HttpGet("{id}")]
        public ActionResult GetDataTable(string id)
        {
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var dataTable = (
                from DataTable in db.DataTable
                where DataTable.DataTableID == id
                select new
                {
                DataTable.DataTableID, DataTable.DataTableName, DataTable.CreatedOn, DataTable.CreatedBy, DataTable.LastModifiedOn, DataTable.LastModifiedBy
                }

            ).FirstOrDefault();
            if (dataTable == null)
            {
                return NotFound();
            }

            dynamic record = dataTable.ToExpando();
            return Ok(record);
        }

        // PUT api/DataTable/5
        [HttpPut("{id}")]
        public ActionResult PutDataTable(string id, DataTable dataTable)
        {
            ModelState.Clear();
            Extensions.ClearReferences(dataTable);
            TryValidateModel(dataTable);
            if (ModelState.IsValid && id == dataTable.DataTableID)
            {
                db.Entry(dataTable).State = EntityState.Modified;
                Model.Entities.ProcessChildrenUpdate(db, dataTable.DataTranslation.ToList());
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

        // POST api/DataTable
        [HttpPost]
        public ActionResult<DataTable> PostDataTable(DataTable dataTable)
        {
            ModelState.Clear();
            Extensions.ClearReferences(dataTable);
            TryValidateModel(dataTable);
            if (ModelState.IsValid)
            {
                db.DataTable.Add(dataTable);
                Model.Entities.ProcessChildrenUpdate(db, dataTable.DataTranslation.ToList());
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (DataTableExists(dataTable.DataTableID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetDataTable", new
                {
                id = dataTable.DataTableID
                }

                , dataTable);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/DataTable/5
        [HttpDelete("{id}")]
        public ActionResult<DataTable> DeleteDataTable(string id)
        {
            DataTable dataTable = db.DataTable.Find(id);
            if (dataTable == null)
            {
                return NotFound();
            }

            db.DataTable.Remove(dataTable);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return dataTable;
        }

        private bool DataTableExists(string id)
        {
            return db.DataTable.Any(e => e.DataTableID == id);
        }
    }
}