using CabernetDBContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EmmploymeNet.Model
{
    public partial class File  : IEntityRecord
    {
        public File()
        {
            JobRequestFile = new HashSet<JobRequestFile>();
            User = new HashSet<User>();
        }


        
        public string FileID { get; set; }

        
        public string FileName { get; set; }

        
        public string FolderName { get; set; }

        
        public DateTimeOffset? FileDate { get; set; }

        
        public int? FileSize { get; set; }

        
        public DateTimeOffset? CreatedOn { get; set; }

        
        public string CreatedBy { get; set; }

        [ConcurrencyCheck]

        public DateTimeOffset? LastModifiedOn { get; set; }

        
        public string LastModifiedBy { get; set; }

        public virtual ICollection<JobRequestFile> JobRequestFile { get; set; }
        public virtual ICollection<User> User { get; set; }

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
