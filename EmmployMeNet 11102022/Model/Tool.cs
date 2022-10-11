using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

namespace EmmploymeNet.Model
{
    public partial class Tool  : IEntityRecord
    {
        public Tool()
        {
            JobRequestTool = new HashSet<JobRequestTool>();
        }

        [Key]
        [StringLength(36)]

        
        public string ToolID { get; set; }
        [Required]
        [StringLength(200)]

        
        public string ToolName { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(200)]

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }
        [StringLength(200)]

        
        public string LastModifiedBy { get; set; }

        [InverseProperty("Tool")]
        public virtual ICollection<JobRequestTool> JobRequestTool { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
