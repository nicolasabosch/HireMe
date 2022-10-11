using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

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

        [Key]

        
        public int JobRequestID { get; set; }
        [Required]
        [StringLength(200)]

        
        public string JobRequestName { get; set; }
        [Required]
        [StringLength(2000)]

        
        public string JobRequestDescription { get; set; }

        
        public int CompanyID { get; set; }
        [Required]
        [StringLength(36)]

        
        public string JobCategoryID { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(200)]

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }
        [StringLength(200)]

        
        public string LastModifiedBy { get; set; }

        [ForeignKey(nameof(CompanyID))]
        [InverseProperty("JobRequest")]
        public virtual Company Company { get; set; }
        [ForeignKey(nameof(JobCategoryID))]
        [InverseProperty("JobRequest")]
        public virtual JobCategory JobCategory { get; set; }
        [InverseProperty("JobRequest")]
        public virtual ICollection<JobRequestFile> JobRequestFile { get; set; }
        [InverseProperty("JobRequest")]
        public virtual ICollection<JobRequestTool> JobRequestTool { get; set; }
        [InverseProperty("JobRequest")]
        public virtual ICollection<JobRequestUser> JobRequestUser { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
