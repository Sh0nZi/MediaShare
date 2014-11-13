namespace MediaShare.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MediaShare.Data.Migrations;
    using MediaShare.Models;

    public class MediaShareContext : IdentityDbContext<ApplicationUser>, IMediaShareContext
    {
        public MediaShareContext() : base("MediaShareConnectionString", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MediaShareContext, Configuration>());
        }
        
        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Vote> Votes { get; set; }

        public IDbSet<MediaFile> Files { get; set; }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return this.DbContext.Entry(entity);
        }

        public static MediaShareContext Create()
        {
            return new MediaShareContext();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}