namespace MediaShare.Web.Areas.Admin.Models
{
    using System.Web.Mvc;
    using AutoMapper;
    using MediaShare.Models;
    using MediaShare.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class UserAdminViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [HiddenInput(DisplayValue = false)] 
        public string Email { get; set; }

        public bool IsBanned { get; set; }

        [HiddenInput(DisplayValue = false)] 
        public int FilesUploaded { get; set; }

        [HiddenInput(DisplayValue = false)] 
        public int CommentsPosted { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, UserAdminViewModel>()
                         .ForMember(m => m.FilesUploaded,
                              opt => opt.MapFrom(u => u.Files.Count));
            configuration.CreateMap<ApplicationUser, UserAdminViewModel>()
                         .ForMember(m => m.CommentsPosted,
                              opt => opt.MapFrom(u => u.Comments.Count));
        }
    }
}