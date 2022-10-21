using CabernetDBContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EmmploymeNet.Model
{
    public partial class JobRequest  : IEntityRecord
    {
        public JobRequest()
        {
            JobRequestFile = new HashSet<JobRequestFile>();
            JobRequestTool = new HashSet<JobRequestTool>();
            JobRequestUser = new HashSet<JobRequestUser>();
        }


        
        public int JobRequestID { get; set; }

        
        public string JobRequestName { get; set; }

        
        public string JobRequestDescription { get; set; }

        
        public int CompanyID { get; set; }

        
        public string JobCategoryID { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }

        
        public string LastModifiedBy { get; set; }

        public virtual Company Company { get; set; }
        public virtual JobCategory JobCategory { get; set; }
        public virtual ICollection<JobRequestFile> JobRequestFile { get; set; }
        public virtual ICollection<JobRequestTool> JobRequestTool { get; set; }
        public virtual ICollection<JobRequestUser> JobRequestUser { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }

    	[NotMapped]
    	public Dictionary<string, object>
    OriginalValues { get; set; }

    [NotMapped]
    public virtual ICollection<DataTranslation>
        DataTranslation { get; set; }
        }
        }
