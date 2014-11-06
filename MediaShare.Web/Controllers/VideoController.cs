using MediaShare.Data;
using MediaShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MediaShare.Web.Helpers;

namespace MediaShare.Web.Controllers
{
    public class VideoController : MediaFileController
    {
        public VideoController(IMediaShareData data) : base(data)
        {
        }

        // GET: Video
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult UploadVideo(MediaFile file, HttpPostedFileBase mediaFile)
        {
            if (mediaFile == null)
            {
                ModelState.AddModelError("Content", "Content is required");
                return View("Upload", file);
            }

            if (mediaFile != null && mediaFile.ContentLength > 50000000)
            {
                ModelState.AddModelError("Size", "Your file is too large! Allowed size 50 mb");
                return View("Upload", file);
            }
            if (mediaFile != null && mediaFile.ContentType != "video/mp4")
            {
                ModelState.AddModelError("Type", "Your file is in wrong format! Allowed format - mp4");
                return View("Upload", file);
            }

            this.PopulateContent(file, mediaFile);
            file.Thumbnail = ThumbnailExtractor.GetVideoThumbnail(file.Content);
            file.Type = MediaType.Video;
            //Entity Framework does not allow to store files larger than 50mb...
             
            this.Data.Files.Add(file);
            this.Data.SaveChanges();
            TempData["Success"] = "Video successfully added!";
            return RedirectToAction("Index", "Home");
            
        }
        
        public ActionResult ContentById(int id)
        {
            var content = this.Data.Files.All().FirstOrDefault(x => x.Id == id).Content;                     
            return File(content, "video/mp4");
        }
    }
}