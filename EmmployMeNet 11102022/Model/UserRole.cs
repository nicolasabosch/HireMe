using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

namespace EmmploymeNet.Model
{
    public partial class UserRole  : IEntityRecord
    {
        [Key]
        [StringLength(36)]

        
        public string UserID { get; set; }
        [Key]
        [StringLength(36)]

        
        public string RoleID { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(200)]

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }
        [StringLength(200)]

        
        public string LastModifiedBy { get; set; }

        [ForeignKey(nameof(RoleID))]
        [InverseProperty("UserRole")]
        public virtual Role Role { get; set; }
        [ForeignKey(nameof(UserID))]
        [InverseProperty("UserRole")]
        public virtual User User { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
