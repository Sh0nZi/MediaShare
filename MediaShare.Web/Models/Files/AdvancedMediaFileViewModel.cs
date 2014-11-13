namespace MediaShare.Web.Models.Files
{
    using System;
    using AutoMapper;
    using MediaShare.Web.Infrastructure.Mapping;
    using MediaShare.Models;
    using MediaShare.Web.Areas.Authorized.Models;

    public class AdvancedMediaFileViewModel : BasicMediaFileViewModel, IMapFrom<MediaFile>
    {
        public int ViewsCount { get; set; }

        public virtual UserViewModel Author { get; set; }
        //public void CreateMappings(IConfiguration configuration)
        //{
        //      configuration.CreateMap<MediaFile, AdvancedMediaFileViewModel>()
        //        .ForMember(m => m.Author,
        //        opt => opt.MapFrom(u => u.Author));
        //}
    }
}