using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace MediaShare.Models
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<MediaFile> favourites;

        public ApplicationUser()
        {
            this.favourites = new HashSet<MediaFile>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<MediaFile> Favourites
        {
            get
            {
                return this.favourites;
            }
            set
            {
                this.favourites = value;
            }
        }
    }
}