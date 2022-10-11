using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

namespace EmmploymeNet.Model
{
    public partial class Parameter  : IEntityRecord
    {
        [Key]
        [StringLength(50)]

        
        public string ParameterID { get; set; }
        [Required]
        [StringLength(200)]

        
        public string ParameterName { get; set; }
        [StringLength(2000)]

        
        public string ParameterDescription { get; set; }

        
        public string ParameterValue { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(200)]

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }
        [StringLength(200)]

        
        public string LastModifiedBy { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
