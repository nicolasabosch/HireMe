using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

namespace EmmploymeNet.Model
{
    public partial class RoleMenuItem  : IEntityRecord
    {
        [Key]
        [StringLength(36)]

        
        public string RoleID { get; set; }
        [Key]
        [StringLength(36)]

        
        public string MenuItemID { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(200)]

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }
        [StringLength(200)]

        
        public string LastModifiedBy { get; set; }

        [ForeignKey(nameof(MenuItemID))]
        [InverseProperty("RoleMenuItem")]
        public virtual MenuItem MenuItem { get; set; }
        [ForeignKey(nameof(RoleID))]
        [InverseProperty("RoleMenuItem")]
        public virtual Role Role { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
