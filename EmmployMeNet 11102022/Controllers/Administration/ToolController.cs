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
    [Route("api/Tool")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        private readonly Entities db;
        public ToolController(Entities context)
        {
            db = context;
        }

        // GET api/Tool
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAll()
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            
            var list = (
                from Tool in db.Tool
                select new
                {
                Tool.ToolID, Tool.ToolName, }

            );
            if (parameters["key"] != null)
            {
                string key = parameters["key"];
                list = list.Where(l => l.ToolID == key);
            }

            if (parameters["ToolID"] != null)
            {
                string toolID = parameters["ToolID"];
                list = list.Where(l => l.ToolID == toolID);
            }

            if (parameters["ToolName"] != null)
            {
                string toolName = parameters["ToolName"];
                list = list.Where(l => l.ToolName.Contains(toolName));
                list = list.OrderBy(l => l.ToolName.IndexOf(toolName));
            }

            var ret = list.AsEnumerable();
            return Ok(ret);
        }

        // GET api/Tool/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult GetTool(string id)
        {
            var languageID = this.CurrentLanguageID();
            var previewList = new[]{".jpeg", ".jpg", ".png", ".gif", ".bmp"};
            var tool = (
                from Tool in db.Tool
                where Tool.ToolID == id
                select new
                {
                Tool.ToolID, Tool.ToolName, Tool.CreatedOn, Tool.CreatedBy, Tool.LastModifiedOn, Tool.LastModifiedBy
                }

            ).FirstOrDefault();
            if (tool == null)
            {
                return NotFound();
            }

            dynamic record = tool.ToExpando();
            return Ok(record);
        }

        // PUT api/Tool/5
        [HttpPut("{id}")]
        [AllowAnonymous]
        public ActionResult PutTool(string id, Tool tool)
        {
            ModelState.Clear();
            Extensions.ClearReferences(tool);
            TryValidateModel(tool);
            if (ModelState.IsValid && id == tool.ToolID)
            {
                db.Entry(tool).State = EntityState.Modified;
                
                Entities.ProcessChildrenUpdate(db, tool.DataTranslation.ToList());
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

        // POST api/Tool
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Tool> PostTool(Tool tool)
        {
            ModelState.Clear();
            Extensions.ClearReferences(tool);
            TryValidateModel(tool);
            if (ModelState.IsValid)
            {
                db.Tool.Add(tool);
                Entities.ProcessChildrenUpdate(db, tool.DataTranslation.ToList());
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (ToolExists(tool.ToolID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetTool", new
                {
                id = tool.ToolID
                }

                , tool);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/Tool/5
        [HttpDelete("{id}")]
        public ActionResult<Tool> DeleteTool(string id)
        {
            Tool tool = db.Tool.Find(id);
            if (tool == null)
            {
                return NotFound();
            }

            db.Tool.Remove(tool);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return tool;
        }

        private bool ToolExists(string id)
        {
            return db.Tool.Any(e => e.ToolID == id);
        }
    }
}