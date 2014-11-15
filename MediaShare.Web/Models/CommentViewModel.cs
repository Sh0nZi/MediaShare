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
        [ScaffoldColumn(false)]
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

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result * 23 + this.Id.GetHashCode();
                result = result * 23 + ((Content != null) ? this.Content.GetHashCode() : 0);
                result = result * 23 + this.DateCreated.GetHashCode();
                result = result * 23 + this.MediaFileId.GetHashCode();
                result = result * 23 + ((MediaFileTitle != null) ? this.MediaFileTitle.GetHashCode() : 0);
                result = result * 23 + ((AuthorId != null) ? this.AuthorId.GetHashCode() : 0);
                result = result * 23 + ((AuthorName != null) ? this.AuthorName.GetHashCode() : 0);
                return result;
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                         .ForMember(m => m.AuthorName,
                              opt => opt.MapFrom(u => u.Author.Email));
            configuration.CreateMap<Comment, CommentViewModel>()
                         .ForMember(m => m.MediaFileTitle,
                              opt => opt.MapFrom(u => u.MediaFile.Title));
        }

        public override bool Equals(object obj)
        {
            var o = obj as CommentViewModel;
            return o.Id == this.Id;
        }
    }
}