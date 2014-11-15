using AutoMapper;
using MediaShare.Models;
using MediaShare.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediaShare.Web.Areas.Admin.Models
{
    public class FileAdminViewModel : IMapFrom<MediaFile>, IHaveCustomMappings
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss}")]
        public DateTime DateCreated { get; set; }

        public MediaType Type { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<MediaFile, FileAdminViewModel>()
                         .ForMember(m => m.AuthorName,
                              opt => opt.MapFrom(u => u.Author.Email));
        }
    }
}