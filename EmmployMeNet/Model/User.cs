using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

namespace EmmploymeNet.Model
{
    public partial class User  : IEntityRecord
    {
        public User()
        {
            JobRequestUser = new HashSet<JobRequestUser>();
            UserRole = new HashSet<UserRole>();
        }

        [Key]
        [StringLength(36)]

        
        public string UserID { get; set; }
        [Required]
        [StringLength(100)]

        
        public string LogonName { get; set; }



        


        [Required]
        [StringLength(100)]

        
        public string Password { get; set; }
        [Required]
        [StringLength(100)]

        
        public string UserName { get; set; }

        
        public bool Active { get; set; }

        
        public bool? ForceChangePassword { get; set; }


        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        
        public DateTimeOffset? LastLogon { get; set; }

        
        public bool ReceiveNotification { get; set; }
        [StringLength(36)]

        
        public string ResetPasswordID { get; set; }

        
        public DateTimeOffset? InvitedOn { get; set; }
        [Required]
        [StringLength(36)]

        
        public string UserTypeID { get; set; }
        [StringLength(10)]

        
        public string ZipCode { get; set; }
        [Column(TypeName = "date")]

        
        public DateTime? BirthDate { get; set; }
        [StringLength(36)]

        
        public string FileID { get; set; }

        
        public int? CompanyID { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(200)]

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }
        [StringLength(200)]

        
        public string LastModifiedBy { get; set; }

        [ForeignKey(nameof(CompanyID))]
        [InverseProperty("User")]
        public virtual Company Company { get; set; }
        [ForeignKey(nameof(FileID))]
        [InverseProperty("User")]
        public virtual File File { get; set; }
        [ForeignKey(nameof(UserTypeID))]
        [InverseProperty("User")]
        public virtual UserType UserType { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<JobRequestUser> JobRequestUser { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserRole> UserRole { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
