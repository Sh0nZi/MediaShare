using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using MediaShare.Data.Repositories;
    using MediaShare.Models;

    public class MediaShareData : IMediaShareData
    {
        private readonly DbContext context;

        private readonly IDictionary<Type, object> repositories;

        public MediaShareData() : this(new MediaShareContext())
        {
        }
        
        public MediaShareData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public IRepository<MediaFile> Files
        {
            get
            {
                return this.GetRepository<MediaFile>();
            }
        }
        
        public IRepository<Vote> Votes
        {
            get
            {
                return this.GetRepository<Vote>();
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public int SaveChanges()
        {
            while (true)
            {
                try
                {
                    return this.context.SaveChanges();
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(Repository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}