namespace MediaShare.Web.Models.Files
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MediaShare.Models;
    using MediaShare.Web.Infrastructure.Mapping;

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