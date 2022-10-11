using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

namespace EmmploymeNet.Model
{
    public partial class Company  : IEntityRecord
    {
        public Company()
        {
            JobRequest = new HashSet<JobRequest>();
            User = new HashSet<User>();
        }

        [Key]

        
        public int CompanyID { get; set; }
        [Required]
        [StringLength(200)]

        
        public string CompanyName { get; set; }
        [Required]
        [StringLength(36)]

        
        public string CompanyTypeID { get; set; }
        [StringLength(233)]

        
        public string CompanyFullName { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(200)]

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }
        [StringLength(200)]

        
        public string LastModifiedBy { get; set; }

        [ForeignKey(nameof(CompanyTypeID))]
        [InverseProperty("Company")]
        public virtual CompanyType CompanyType { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<JobRequest> JobRequest { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<User> User { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
