namespace MediaShare.Web.Controllers
{
    using System.Collections.Generic;
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
        private const int PageSize = 5;

        public AllFilesController(IMediaShareData data)
            : base(data)
        {

        }

        // GET: AllFiles
        public ActionResult Index()
        {
            var videos = this.MediaFiles.Project().To<AdvancedMediaFileViewModel>().OrderByDescending(f => f.DateCreated);

            this.IsSameInSession("type", "Both");
            this.IsSameInSession("searchString", string.Empty);
            return this.View(videos.ToPagedList(1, PageSize));
        }

        [HttpGet]
        public ActionResult RenderFiles(int? page, FilterViewModel filter)
        {
            var allFiles = this.MediaFiles.Project().To<AdvancedMediaFileViewModel>();
            
            if (!this.IsSameInSession("type", filter.Type))
            {
                allFiles = this.Filter(allFiles, filter.Type);
            }
                
            if (!this.IsSameInSession("searchString", filter.SearchString))
            {
                allFiles = allFiles.Where(s => s.Title.ToUpper()
                        .Contains(filter.SearchString != null ? filter.SearchString.ToUpper() : string.Empty));
            }
            
            allFiles = this.Sort(allFiles, filter.OrderBy, filter.SortType);
            
            

            return this.PartialView("_PagedFiles", allFiles.ToPagedList(page ?? 1, PageSize));
        }

        private IQueryable<AdvancedMediaFileViewModel> Filter(IQueryable<AdvancedMediaFileViewModel> files, string filterType)
        {           
            if (filterType == "Video")
            {
                return files.Where(s => s.Type == MediaType.Video);
            }

            if (filterType == "Audio")
            {
                return files.Where(s => s.Type == MediaType.Audio);
            }

            return files;
        }

        private IQueryable<AdvancedMediaFileViewModel> Sort(IQueryable<AdvancedMediaFileViewModel> files, string orderBy, string sortType)
        {

            switch (orderBy)
            {
                case "Views":
                    {
                        if (sortType == "Ascending")
                        {
                            return files.OrderBy(f => f.ViewsCount);
                        }
                        else
                        {
                            return  files.OrderByDescending(f => f.ViewsCount);
                        }
                    }
                case "Rating":
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
                default:
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
        }

        private bool IsSameInSession(string key, string value)
        {
            value = value ?? (this.Session[key] != null ? this.Session[key].ToString() : null);
            if (this.Session[key] != null && this.Session[key].ToString() == value)
            {
                return true;
            }

            this.Session[key] = value;
            return false;
        }

        private void ResetSession(params string[] keys)
        {
            foreach (var key in keys)
            {
                this.Session[key] = null;
            }
        }
    }
}