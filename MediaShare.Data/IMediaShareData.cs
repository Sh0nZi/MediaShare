namespace MediaShare.Data
{
    using System.Data.Entity;
    using MediaShare.Data.Repositories;
    using MediaShare.Models;

    public interface IMediaShareData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Vote> Votes { get; }
        
        IRepository<MediaFile> Files { get; }

        IRepository<Comment> Comments { get; }

        IMediaShareContext Context { get; }
        
        int SaveChanges();
    }
}