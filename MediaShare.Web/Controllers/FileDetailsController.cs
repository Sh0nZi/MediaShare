namespace MediaShare.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;

    using AutoMapper.QueryableExtensions;

    using MediaShare.Data;
    using MediaShare.Web.Models;
    using MediaShare.Web.Models.Files;

    public class FileDetailsController : BaseController
    {
        public FileDetailsController(IMediaShareData data) : base(data)
        {
        }

        // GET: MediaFileDetails
        public ActionResult Details(int? id)
        {
            var currentUser = this.User.Identity.GetUserId();
            if (currentUser != null)
            {
                this.ViewBag.IsFavourited = this.Data.Users.Find(currentUser).Favourites.Any(f => f.Id == id);
            }
            else
            {
                this.ViewBag.IsFavourited = false;
            }

            if (id == null)
            {
                return this.RedirectToAction("Index", "Home");
            }
            var file = this.MediaFiles.Project().To<MediaFileViewModel>()
                .FirstOrDefault(f => f.Id == id);

            if (file.AuthorId == currentUser)
            {
                this.ViewBag.IsYours = true;
            }
            else
            {
                this.ViewBag.IsYours = false;
            }

            return this.View(file);
        }

        // GET: Votes
        public ActionResult FileVotes(int id)
        {
            var file = this.MediaFiles.Project().To<MediaFileViewModel>()
                .FirstOrDefault(f => f.Id == id);
            return this.PartialView(file);
        }

        public ActionResult FileComments(int id)
        {
            var comments = this.MediaFiles.FirstOrDefault(f => f.Id == id).Comments
                               .OrderByDescending(c => c.DateCreated).AsQueryable()
                               .Project().To<CommentViewModel>().ToList();
            return this.PartialView(comments);
        }

        public ActionResult AudioContentById(int id)
        {
            var content = this.MediaFiles.Project().To<MediaFileViewModel>()
                .FirstOrDefault(x => x.Id == id).Content;           
            return this.File(content, "audio/mp3");
        }

        public ActionResult VideoContentById(int id)
        {
            var content = this.MediaFiles.Project().To<MediaFileViewModel>().
                FirstOrDefault(x => x.Id == id).Content;                     
            return this.File(content, "video/mp4");
        }

        public void IncreaseViewCount(int id)
        {
            this.MediaFiles.FirstOrDefault(f => f.Id == id).ViewsCount++;
            this.Data.SaveChanges();
        }
    }
}