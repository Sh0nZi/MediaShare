namespace MediaShare.Web.Models.Files
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations; 

    using MediaShare.Web.Infrastructure.Mapping;
    using MediaShare.Models;

    public class MediaFileViewModel : IMapFrom<MediaFile>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(50000000)]     
        public byte[] Content { get; set; }
        
        public string Description { get; set; }

        [Required]
        public MediaType Type { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        
        [Required]
        public string AuthorId { get; set; }

        public byte[] Thumbnail { get; set; }

        public int ViewsCount { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
