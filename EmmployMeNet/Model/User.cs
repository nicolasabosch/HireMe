using CabernetDBContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EmmploymeNet.Model
{
    public partial class User  : IEntityRecord
    {
        public User()
        {
            JobApplicance = new HashSet<JobApplicance>();
            JobPost = new HashSet<JobPost>();
            JobRequestUser = new HashSet<JobRequestUser>();
            UserRole = new HashSet<UserRole>();
        }


        
        public string UserID { get; set; }

        
        public string LogonName { get; set; }

        
        public string Password { get; set; }

        
        public string UserName { get; set; }

        
        public bool Active { get; set; }

        
        public bool? ForceChangePassword { get; set; }

        
        public string Email { get; set; }

        
        public DateTimeOffset? LastLogon { get; set; }

        
        public bool ReceiveNotification { get; set; }

        
        public string ResetPasswordID { get; set; }

        
        public DateTimeOffset? InvitedOn { get; set; }

        
        public string UserTypeID { get; set; }

        
        public string ZipCode { get; set; }

        
        public DateTime? BirthDate { get; set; }

        
        public string FileID { get; set; }

        
        public int? CompanyID { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }

        
        public string LastModifiedBy { get; set; }

        public virtual Company Company { get; set; }
        public virtual File File { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<JobApplicance> JobApplicance { get; set; }
        public virtual ICollection<JobPost> JobPost { get; set; }
        public virtual ICollection<JobRequestUser> JobRequestUser { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }

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
