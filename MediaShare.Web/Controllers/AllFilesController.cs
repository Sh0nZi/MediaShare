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

        protected IEnumerable<AdvancedMediaFileViewModel> AllFiles
        {
            get
            {
                if (this.Session["AllFiles"] == null)
                {
                    this.Session["AllFiles"] = this.MediaFiles.Project().To<AdvancedMediaFileViewModel>().OrderByDescending(f => f.DateCreated);
                }
                return this.Session["AllFiles"] as IEnumerable<AdvancedMediaFileViewModel>;
            }
            set
            {
                this.Session["AllFiles"] = value;
            }
        }

        // GET: AllFiles
        public ActionResult Index()
        {
            var videos = this.AllFiles.OrderByDescending(f => f.DateCreated);
            return this.View(videos.ToPagedList(1, PageSize));
        }

        [HttpGet]
        public ActionResult RenderFiles(int? page, FilterViewModel filter)
        {
            if (filter.OrderBy != null)
            {
                if (!this.IsSameInSession("type", filter.Type))
                {
                    ResetSession("AllFiles", "searchString", "orderType", "sortType", filter.Type);
                }

                if (!this.IsSameInSession("searchString", filter.SearchString))
                {
                    ResetSession("AllFiles", "searchString", "orderType", "sortType", filter.Type);
                    AllFiles = AllFiles.Where(s => s.Title.ToUpper()
                           .Contains(filter.SearchString != null ? filter.SearchString.ToUpper() : string.Empty));
                }

                if (!this.IsSameInSession("orderBy", filter.OrderBy)
                    || !this.IsSameInSession("sortType", filter.SortType))
                {
                    this.Sort(filter.OrderBy, filter.SortType);
                }
            }

            return this.PartialView("_PagedFiles", AllFiles.ToPagedList(page ?? 1, PageSize));
        }

        private void Filter(string filterType)
        {
            if (filterType != null)
            {
                if (filterType == "Video")
                {
                    AllFiles = AllFiles.Where(s => s.Type == MediaType.Video);
                }

                if (filterType == "Audio")
                {
                    AllFiles = AllFiles.Where(s => s.Type == MediaType.Audio);
                }
            }
        }

        private void Sort(string orderBy, string sortType)
        {

            switch (orderBy)
            {
                case "Views":
                    {
                        if (sortType == "Ascending")
                        {
                            AllFiles = AllFiles.OrderBy(f => f.ViewsCount);
                        }
                        else
                        {
                            AllFiles = AllFiles.OrderByDescending(f => f.ViewsCount);
                        }
                    }
                    break;
                case "Rating":
                    {
                        if (sortType == "Ascending")
                        {
                            AllFiles = AllFiles.OrderBy(f => (double)f.Votes.Sum(v => v.Value) / f.Votes.Count);
                        }
                        else
                        {
                            AllFiles = AllFiles.OrderByDescending(f => (double)f.Votes.Sum(v => v.Value) / f.Votes.Count);
                        }
                    }
                    break;
                default:
                    {
                        if (sortType == "Ascending")
                        {
                            AllFiles = AllFiles.OrderBy(f => f.DateCreated);
                        }
                        else
                        {
                            AllFiles = AllFiles.OrderByDescending(f => f.DateCreated);
                        }
                    }
                    break;
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
            this.Filter(keys.Last());
        }
    }
}