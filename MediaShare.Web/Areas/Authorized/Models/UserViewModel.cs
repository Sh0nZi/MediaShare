namespace MediaShare.Web.Areas.Authorized.Models
{
    using System.Collections.Generic;

    using MediaShare.Models;
    using MediaShare.Web.Infrastructure.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public ICollection<MediaFile> Favourites { get; set; }
    }
}