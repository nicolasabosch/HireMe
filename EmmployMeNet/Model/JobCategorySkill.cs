using CabernetDBContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EmmploymeNet.Model
{
    public partial class JobCategorySkill  : IEntityRecord
    {
        public JobCategorySkill()
        {
            JobPostSkill = new HashSet<JobPostSkill>();
        }


        
        public int JobCategorySkillID { get; set; }

        
        public string JobCategorySkillName { get; set; }

        
        public string JobCategoryID { get; set; }

        
        public string Remarks { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }

        
        public string LastModifiedBy { get; set; }

        public virtual JobCategory JobCategory { get; set; }
        public virtual ICollection<JobPostSkill> JobPostSkill { get; set; }

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
