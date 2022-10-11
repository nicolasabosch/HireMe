using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

namespace EmmploymeNet.Model
{
    public partial class MenuItem  : IEntityRecord
    {
        public MenuItem()
        {
            RoleMenuItem = new HashSet<RoleMenuItem>();
        }

        [Key]
        [StringLength(36)]

        
        public string MenuItemID { get; set; }
        [Required]
        [StringLength(100)]

        
        public string MenuItemName { get; set; }
        [Required]
        [StringLength(36)]

        
        public string MenuBarID { get; set; }

        
        public short DisplayOrder { get; set; }

        
        public short GroupNumber { get; set; }
        [Required]
        [StringLength(200)]

        
        public string RouteName { get; set; }

        
        public bool IsPage { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(200)]

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }
        [StringLength(200)]

        
        public string LastModifiedBy { get; set; }

        [InverseProperty("MenuItem")]
        public virtual ICollection<RoleMenuItem> RoleMenuItem { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
