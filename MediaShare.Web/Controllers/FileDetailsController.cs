namespace MediaShare.Web.Controllers
{
    using System.Linq;
    using System.Security.Principal;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using AutoMapper.QueryableExtensions;
    using MediaShare.Data;
    using MediaShare.Web.Models;
    using MediaShare.Web.Models.Files;
    using MediaShare.Web.Infrastructure.Helpers;
    using System.Net;
    using System.Threading.Tasks;
    using System;

    public class FileDetailsController : BaseController
    {
        private readonly IIdentity identity;

        public FileDetailsController(IMediaShareData data, IIdentity identity) : base(data)
        {
            this.identity = identity;
        }

        // GET: MediaFileDetails
        public ActionResult Details(int? id)
        {
            var currentUser = this.identity.GetUserId();
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
            
         
            ViewBag.MediaLink = DropboxHandler.GetUrl(file.Content);
            ViewBag.ThumbLink = DropboxHandler.GetUrl(file.Thumbnail);
			if (file.Type == MediaType.Audio)
            {
                ViewBag.ThumbLink = DropboxHandler.GetUrl("Audio Symbol.png");
            }
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
            var comments = this.MediaFiles.Project().To<MediaFileViewModel>()
                               .FirstOrDefault(f => f.Id == id).Comments.AsQueryable()
                               .Project().To<CommentViewModel>().OrderByDescending(c => c.DateCreated)
                               .ToList();
            return this.PartialView(comments);
        }

        public void IncreaseViewCount(int id)
        {
            this.MediaFiles.FirstOrDefault(f => f.Id == id).ViewsCount++;
            this.Data.SaveChanges();
        }
    }
}