using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MediaShare.Models;
using MediaShare.Web.Infrastructure.Mapping;

namespace MediaShare.Web.Models.Files
{
    public class BasicMediaFileViewModel : IMapFrom<MediaFile>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        
        [Required]
        public string AuthorId { get; set; }

        public ICollection<Vote> Votes { get; set; }
        
        [Required]
        public MediaType Type { get; set; }
    }
}