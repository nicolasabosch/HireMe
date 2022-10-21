using CabernetDBContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EmmploymeNet.Model
{
    public partial class JobCategory  : IEntityRecord
    {
        public JobCategory()
        {
            JobCategorySkill = new HashSet<JobCategorySkill>();
            JobPost = new HashSet<JobPost>();
            JobRequest = new HashSet<JobRequest>();
        }


        
        public string JobCategoryID { get; set; }

        
        public string JobCategoryName { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }

        
        public string LastModifiedBy { get; set; }

        public virtual ICollection<JobCategorySkill> JobCategorySkill { get; set; }
        public virtual ICollection<JobPost> JobPost { get; set; }
        public virtual ICollection<JobRequest> JobRequest { get; set; }

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
