namespace MediaShare.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using MediaShare.Models;
    using MediaShare.Web.Infrastructure.Mapping;
    using AutoMapper;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss}")]
        public DateTime DateCreated { get; set; }

        [Required]
        public int MediaFileId { get; set; }

        [Required]
        public string MediaFileTitle { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                         .ForMember(m => m.AuthorName,
                              opt => opt.MapFrom(u => u.Author.Email));
            configuration.CreateMap<Comment, CommentViewModel>()
                         .ForMember(m => m.MediaFileTitle,
                              opt => opt.MapFrom(u => u.MediaFile.Title));
        }
    }
}