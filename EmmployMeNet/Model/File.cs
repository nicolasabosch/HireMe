using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabernetDBContext;

namespace EmmploymeNet.Model
{
    public partial class File  : IEntityRecord
    {
        public File()
        {
            JobRequestFile = new HashSet<JobRequestFile>();
            User = new HashSet<User>();
        }

        [Key]
        [StringLength(36)]

        
        public string FileID { get; set; }
        [Required]
        [StringLength(500)]

        
        public string FileName { get; set; }
        [Required]
        [StringLength(500)]

        
        public string FolderName { get; set; }

        
        public DateTimeOffset? FileDate { get; set; }

        
        public int? FileSize { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(200)]

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }
        [StringLength(200)]

        
        public string LastModifiedBy { get; set; }

        [InverseProperty("File")]
        public virtual ICollection<JobRequestFile> JobRequestFile { get; set; }
        [InverseProperty("File")]
        public virtual ICollection<User> User { get; set; }

        [NotMapped]
    	public string EntityStatus { get; set; }
    
    	[NotMapped]
    	public Dictionary<string, object> OriginalValues { get; set; }
           
        [NotMapped]
    	public virtual ICollection<DataTranslation> DataTranslation { get; set; }
    }
}
