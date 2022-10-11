using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

namespace EmmploymeNet.Model
{
    public partial class JobCategorySkill  : IEntityRecord
    {
        [Key]

        
        public int JobCategorySkillID { get; set; }
        [Required]
        [StringLength(200)]

        
        public string JobCategorySkillName { get; set; }
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

        [ForeignKey(nameof(JobCategoryID))]
        [InverseProperty("JobCategorySkill")]
        public virtual JobCategory JobCategory { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
