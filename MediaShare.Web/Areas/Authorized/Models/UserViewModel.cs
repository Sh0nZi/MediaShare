using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MediaShare.Web.Infrastructure.Mapping;
using MediaShare.Models;
using MediaShare.Common;

namespace MediaShare.Web.Areas.Authorized.Models
{
    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public ICollection<MediaFile> Favourites { get; set; }
      
    }
}