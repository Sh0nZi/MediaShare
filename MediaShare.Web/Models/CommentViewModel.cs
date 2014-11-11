namespace MediaShare.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using MediaShare.Models;
    using MediaShare.Web.Infrastructure.Mapping;

    public class CommentViewModel :IMapFrom<Comment>
    {

        public int Id { get; set; }

        [Required]
        [Range(10, 300)]
        public string Content { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }
    }
}