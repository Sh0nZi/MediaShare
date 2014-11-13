namespace MediaShare.Web.Areas.Authorized.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using PagedList;
    using AutoMapper.QueryableExtensions;

    using MediaShare.Data;
    using MediaShare.Web.Models.Files;
    using MediaShare.Common;

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
            if (id == this.GetCurrentUser().Id)
            {
                ViewBag.UserName = "Your";
            }
            else
            {
                ViewBag.UserName = this.GetCurrentUser(id).Email.ExtractUsernameFromMail() + " 's";
            }

            var videos = this.MediaFiles.Project().To<BasicMediaFileViewModel>()
                             .Where(f => f.AuthorId == id).OrderByDescending(f => f.DateCreated);
            int pageNumber = (page ?? 1);
            return View(videos.ToPagedList(pageNumber, PageSize));
        }
    }
}