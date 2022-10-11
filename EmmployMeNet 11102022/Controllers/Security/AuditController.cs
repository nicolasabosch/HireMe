using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using System.Collections.Specialized;

using CabernetDBContext;

namespace EmmploymeNet.Model
{
    [Route("api/Audit")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly Entities db;
        public AuditController(Entities context)
        {
            db = context;
        }

        // GET api/Language
        [HttpGet]
        public ActionResult GetAudit(string tableName, string tableKeyValues)
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(this.Request.QueryString.Value);
            var languageID = this.CurrentLanguageID();
            var audit = (from Audit in db.Audit
                         where Audit.TableName == tableName && Audit.TableKeyValues == tableKeyValues
                         from UserCreated in db.User.Where(X => X.UserID == Audit.CreatedBy).DefaultIfEmpty()
                         select new
                         {
                             Audit.CreatedOn,
                             UserFullName = UserCreated == null ? "N/A": UserCreated.UserName,
                             Audit.AuditID
                         }).ToList();

            var auditDetail = (from Audit in db.Audit
                        join AuditDetail in db.AuditDetail on  Audit.AuditID equals AuditDetail.AuditID 
                        where Audit.TableName == tableName && Audit.TableKeyValues == tableKeyValues
                        select new
                        {
                            AuditDetail.AuditID,
                            AuditDetail.FieldName,
                            AuditDetail.OldValue,
                            AuditDetail.NewValue
                                          
                        }).ToList();

             var list = (from Audit in audit
                        
                        select new
                        {
                            Audit.CreatedOn,
                            Audit.UserFullName,
                            AuditDetail = (from AuditDetail in auditDetail where AuditDetail.AuditID == Audit.AuditID
                                          select new {
                                              AuditDetail.FieldName,
                                              AuditDetail.OldValue,
                                              AuditDetail.NewValue
                                          })
                        });


            var ret = list.AsEnumerable();
            return Ok(ret);
        }

    }
}