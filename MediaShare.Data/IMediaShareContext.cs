namespace MediaShare.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using MediaShare.Models;

    public interface IMediaShareContext
    {
        DbContext DbContext { get; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Vote> Votes { get; set; }

        IDbSet<MediaFile> Files { get; set; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();

        IDbSet<T> Set<T>() where T : class;
    }
}