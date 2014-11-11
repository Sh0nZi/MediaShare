namespace MediaShare.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using PagedList;
    using AutoMapper.QueryableExtensions;

    using MediaShare.Data;
    using MediaShare.Models;
    using MediaShare.Web.Models;
    using MediaShare.Web.Models.Files;

    public class AllFilesController : BaseController
    {
        private const int PageSize = 4;

        public AllFilesController(IMediaShareData data) : base(data)
        {
        }

        // GET: AllFiles
        public ActionResult Index()
        {
            var videos = this.MediaFiles.Project().To<MediaFileViewModel>();
            videos = videos.OrderByDescending(f => f.DateCreated);
            return this.View(videos.ToPagedList(1, PageSize));
        }

        [HttpGet]
        public ActionResult RenderFiles(int? page, FilterViewModel filter)
        {
            var files = this.MediaFiles.Project().To<MediaFileViewModel>();

            string filterType = filter.Type;
            string searchString = filter.SearchString;
            string orderBy = filter.OrderBy;
            string sortType = filter.SortType;

            this.SaveToSession("search", ref searchString);
            this.SaveToSession("type", ref filterType);
            this.SaveToSession("orderBy", ref orderBy);
            this.SaveToSession("sortType", ref sortType);

            files = this.Filter(files, searchString, filterType);
            files = this.Sort(files, orderBy, sortType);

            return this.PartialView("_PagedFiles", files.ToPagedList(page ?? 1, PageSize));
        }

        private IQueryable<MediaFileViewModel> Filter(IQueryable<MediaFileViewModel> files, string searchString, string filterType)
        {
             if (!string.IsNullOrEmpty(searchString))
            {
                files = files.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
            }

            if (filterType != null)
            {
                if (filterType == "Video")
                {
                    return files.Where(s => s.Type == MediaType.Video);
                }

                if (filterType == "Audio")
                {
                    return files.Where(s => s.Type == MediaType.Audio);
                }
            }
            return files;

        }

        private IQueryable<MediaFileViewModel> Sort(IQueryable<MediaFileViewModel> files, string orderBy, string sortType)
        {
            
            if (orderBy == "Views")
            {
                if (sortType == "Ascending")
                {
                    return files.OrderBy(f => f.ViewsCount);
                }
                else
                {
                    return files.OrderByDescending(f => f.ViewsCount);
                }
            }
            else if (orderBy == "Rating")
            {
                if (sortType == "Ascending")
                {
                    return files.OrderBy(f => (double)f.Votes.Sum(v => v.Value) / f.Votes.Count);
                }
                else
                {
                    return files.OrderByDescending(f => (double)f.Votes.Sum(v => v.Value) / f.Votes.Count);
                }
            }
            else
            {
                if (sortType == "Ascending")
                {
                    return files.OrderBy(f => f.DateCreated);
                }
                else
                {
                    return files.OrderByDescending(f => f.DateCreated);
                }
            }
        }

        private void SaveToSession(string key, ref string value)
        {
            if (this.Session[key] == null)
            {
                this.Session[key] = string.Empty;
            }

            if (value != null)
            {
                this.Session[key] = value;
            }
            else
            {
                value = this.Session[key].ToString();
            }
        }
    }
}