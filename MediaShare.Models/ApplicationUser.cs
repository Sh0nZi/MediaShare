using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaShare.Models
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<MediaFile> favourites;

        private ICollection<MediaFile> files;

        private ICollection<Comment> comments;

        private ICollection<Vote> votes;

        public ApplicationUser()
        {
            this.favourites = new HashSet<MediaFile>();
            this.files = new HashSet<MediaFile>();
            this.comments = new HashSet<Comment>();
            this.votes = new HashSet<Vote>();
            this.IsBanned = false;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public bool IsBanned { get; set; }

        [InverseProperty("User")]
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

        [InverseProperty("Author")]
        public virtual ICollection<MediaFile> Files
        {
            get
            {
                return this.files;
            }
            set
            {
                this.files = value;
            }
        }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }

        public virtual ICollection<Vote> Votes
        {
            get
            {
                return this.votes;
            }
            set
            {
                this.votes = value;
            }
        }
    }
}