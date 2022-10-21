using CabernetDBContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EmmploymeNet.Model
{
    public partial class MenuItem  : IEntityRecord
    {
        public MenuItem()
        {
            RoleMenuItem = new HashSet<RoleMenuItem>();
        }


        
        public string MenuItemID { get; set; }

        
        public string MenuItemName { get; set; }

        
        public string MenuBarID { get; set; }

        
        public short DisplayOrder { get; set; }

        
        public short GroupNumber { get; set; }

        
        public string RouteName { get; set; }

        
        public bool IsPage { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }

        
        public string LastModifiedBy { get; set; }

        public virtual MenuBar MenuBar { get; set; }
        public virtual ICollection<RoleMenuItem> RoleMenuItem { get; set; }

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
