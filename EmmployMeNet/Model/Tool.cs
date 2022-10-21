using CabernetDBContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EmmploymeNet.Model
{
    public partial class Tool  : IEntityRecord
    {
        public Tool()
        {
            JobRequestTool = new HashSet<JobRequestTool>();
        }


        
        public string ToolID { get; set; }

        
        public string ToolName { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }

        
        public string LastModifiedBy { get; set; }

        public virtual ICollection<JobRequestTool> JobRequestTool { get; set; }

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
