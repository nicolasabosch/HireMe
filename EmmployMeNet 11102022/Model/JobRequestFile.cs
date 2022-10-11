using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

namespace EmmploymeNet.Model
{
    public partial class JobRequestFile  : IEntityRecord
    {
        [Key]
        [StringLength(36)]

        
        public string JobRequestFileID { get; set; }
        [StringLength(200)]

        
        public string JobRequestFileName { get; set; }

        
        public int JobRequestID { get; set; }
        [Required]
        [StringLength(36)]

        
        public string FileID { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(200)]

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }
        [StringLength(200)]

        
        public string LastModifiedBy { get; set; }

        [ForeignKey(nameof(FileID))]
        [InverseProperty("JobRequestFile")]
        public virtual File File { get; set; }
        [ForeignKey(nameof(JobRequestID))]
        [InverseProperty("JobRequestFile")]
        public virtual JobRequest JobRequest { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
