using MediaShare.Data.Repositories;
using MediaShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.Data
{
    public interface IMediaShareData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Vote> Votes { get; }
        
        IRepository<MediaFile> Files { get; }

        IRepository<Comment> Comments { get; }
        
        int SaveChanges();
    }
}