using CabernetDBContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EmmploymeNet.Model
{
    public partial class JobApplicance  : IEntityRecord
    {

        
        public int JobApplicanceID { get; set; }

        
        public string UserID { get; set; }

        
        public int JobPostID { get; set; }

        
        public string JobApplicanceStatusID { get; set; }

        
        public string JobApplicanceFileID { get; set; }

        
        public string JobApplicanceText { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }

        
        public string LastModifiedBy { get; set; }

        public virtual JobApplicanceStatus JobApplicanceStatus { get; set; }
        public virtual JobPost JobPost { get; set; }
        public virtual User User { get; set; }

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
