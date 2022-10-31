using CabernetDBContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EmmploymeNet.Model
{
    public partial class JobPost  : IEntityRecord
    {
        public JobPost()
        {
            JobApplicance = new HashSet<JobApplicance>();
            JobPostSkill = new HashSet<JobPostSkill>();
        }


        
        public int JobPostID { get; set; }

        
        public string JobPostName { get; set; }

        
        public string JobPostDescription { get; set; }

        
        public DateTime JobPostDate { get; set; }

        
        public string UserID { get; set; }

        
        public string JobCategoryID { get; set; }

        
        public string JobPostStatusID { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }

        
        public string LastModifiedBy { get; set; }

        public virtual JobCategory JobCategory { get; set; }
        public virtual JobPostStatus JobPostStatus { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<JobApplicance> JobApplicance { get; set; }
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
