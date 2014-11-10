using MediaShare.Data;
using MediaShare.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaShare.Web.Helpers;
using MediaShare.Web.Models;
using Microsoft.AspNet.Identity;

namespace MediaShare.Web.Controllers
{
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
                ViewBag.IsFavourited = this.Data.Users.Find(currentUser).Favourites.Any(f => f.Id == id);
            }
            else
            {
                ViewBag.IsFavourited = false;
            }

            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var file = this.MediaFiles.FirstOrDefault(f => f.Id == id);

            if (file.AuthorId == currentUser)
            {
                ViewBag.IsYours = true;
            }
            else
            {
                ViewBag.IsYours = false;
            }

            return View(file);
        }

        // GET: Votes
        public ActionResult FileVotes(int id)
        {
            var file = this.MediaFiles.FirstOrDefault(f => f.Id == id);
            return PartialView(file);
        }

        public ActionResult FileComments(int id)
        {
            var comments = this.MediaFiles.FirstOrDefault(f => f.Id == id).Comments
                               .OrderByDescending(c => c.DateCreated)
                               .AsQueryable().Select(CommentViewModel.FromComment)
                               .ToList();           
            return PartialView(comments);
        }

        public ActionResult AudioContentById(int id)
        {
            var content = this.MediaFiles.FirstOrDefault(x => x.Id == id).Content;           
            return File(content, "audio/mp3");
        }

        public ActionResult VideoContentById(int id)
        {
            var content = this.MediaFiles.FirstOrDefault(x => x.Id == id).Content;                     
            return File(content, "video/mp4");
        }

        public void IncreaseCount(int id)
        {
            this.MediaFiles.FirstOrDefault(f=>f.Id==id).ViewsCount++;
            this.Data.SaveChanges();
        }
    }
}