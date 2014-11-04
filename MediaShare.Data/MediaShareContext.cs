using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MediaShare.Models;
using MediaShare.Data.Migrations;

namespace MediaShare.Data
{
    public class MediaShareContext : IdentityDbContext<ApplicationUser>
    {
        public MediaShareContext() : base("MediaShareConnectionString", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MediaShareContext, Configuration>());
        }
        
        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Vote> Votes { get; set; }

        public IDbSet<MediaFile> Files { get; set; }

        public static MediaShareContext Create()
        {
            return new MediaShareContext();
        }
    }
}