using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

namespace EmmploymeNet.Model
{
    public partial class Role  : IEntityRecord
    {
        public Role()
        {
            RoleMenuItem = new HashSet<RoleMenuItem>();
            UserRole = new HashSet<UserRole>();
        }

        [Key]
        [StringLength(36)]

        
        public string RoleID { get; set; }
        [Required]
        [StringLength(100)]

        
        public string RoleName { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(200)]

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }
        [StringLength(200)]

        
        public string LastModifiedBy { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<RoleMenuItem> RoleMenuItem { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<UserRole> UserRole { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
