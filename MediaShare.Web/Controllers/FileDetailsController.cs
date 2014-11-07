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
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var file = this.MediaFiles.FirstOrDefault(f => f.Id == id);
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
    }
}