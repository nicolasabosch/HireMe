using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

namespace EmmploymeNet.Model
{
    public partial class JobRequestTool  : IEntityRecord
    {
        [Key]

        
        public int JobRequestID { get; set; }
        [Key]
        [StringLength(36)]

        
        public string ToolID { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(200)]

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }
        [StringLength(200)]

        
        public string LastModifiedBy { get; set; }

        [ForeignKey(nameof(JobRequestID))]
        [InverseProperty("JobRequestTool")]
        public virtual JobRequest JobRequest { get; set; }
        [ForeignKey(nameof(ToolID))]
        [InverseProperty("JobRequestTool")]
        public virtual Tool Tool { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
