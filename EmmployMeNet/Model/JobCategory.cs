using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

namespace EmmploymeNet.Model
{
    public partial class JobCategory  : IEntityRecord
    {
        public JobCategory()
        {
            JobCategorySkill = new HashSet<JobCategorySkill>();
            JobRequest = new HashSet<JobRequest>();
        }

        [Key]
        [StringLength(36)]

        
        public string JobCategoryID { get; set; }
        [Required]
        [StringLength(200)]

        
        public string JobCategoryName { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(200)]

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }
        [StringLength(200)]

        
        public string LastModifiedBy { get; set; }

        [InverseProperty("JobCategory")]
        public virtual ICollection<JobCategorySkill> JobCategorySkill { get; set; }
        [InverseProperty("JobCategory")]
        public virtual ICollection<JobRequest> JobRequest { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
