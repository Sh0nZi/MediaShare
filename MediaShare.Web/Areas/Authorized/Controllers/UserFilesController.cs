namespace MediaShare.Web.Areas.Authorized.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using PagedList;
    using AutoMapper.QueryableExtensions;

    using MediaShare.Data;
    using MediaShare.Web.Models.Files;

    public class UserFilesController : AuthorizedController
    {
        public UserFilesController(IMediaShareData data)
            : base(data)
        {
        }

        // GET: Authorized/UserFiles
        public ActionResult Index(string id, int? page)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            var userName = this.Data.Users.Find(id).UserName;
            if (id == this.CurrentUser)
            {
                ViewBag.UserName = "Your";
            }
            else
            {
                ViewBag.UserName = userName.Substring(0, userName.IndexOf('@')) + " 's";
            }

            var videos = this.MediaFiles.Project().To<BasicMediaFileViewModel>()
                             .Where(f => f.AuthorId == id).OrderByDescending(f => f.DateCreated);
            int pageNumber = (page ?? 1);
            return View(videos.ToPagedList(pageNumber, PageSize));
        }
    }
}