using CabernetDBContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EmmploymeNet.Model
{
    public partial class Company  : IEntityRecord
    {
        public Company()
        {
            JobRequest = new HashSet<JobRequest>();
            User = new HashSet<User>();
        }


        
        public int CompanyID { get; set; }

        
        public string CompanyName { get; set; }

        
        public string CompanyTypeID { get; set; }

        
        public string CompanyFullName { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }

        
        public string LastModifiedBy { get; set; }

        public virtual CompanyType CompanyType { get; set; }
        public virtual ICollection<JobRequest> JobRequest { get; set; }
        public virtual ICollection<User> User { get; set; }

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
