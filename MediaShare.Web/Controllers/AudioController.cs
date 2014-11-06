using System.IO;
using MediaShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NReco.VideoConverter;
using MediaShare.Data;
using MediaShare.Web.Helpers;

namespace MediaShare.Web.Controllers
{
    [Authorize]
    public class AudioController : MediaFileController
    {
        public AudioController(IMediaShareData data) : base(data)
        {
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAudio(MediaFile file, HttpPostedFileBase mediaFile)
        {
            if (mediaFile == null)
            {
                ModelState.AddModelError("Content", "Content is required");
                return View("Upload", file);
            }

            if (mediaFile.ContentLength > 50000000)
            {
                ModelState.AddModelError("Size", "Your file is too large! Allowed size 50 mb");
                return View("Upload", file);
            }
            if (mediaFile.ContentType != "audio/mp3")
            {
                ModelState.AddModelError("Type", "Your file is in wrong format! Allowed format - mp3");
                return View("Upload", file);
            }

            this.PopulateContent(file, mediaFile);
            file.Type = MediaType.Audio;
            file.Thumbnail = ThumbnailExtractor.GetAudioThumbnail();
            
            this.Data.Files.Add(file);
            this.Data.SaveChanges();
            
            TempData["Success"] = "Audio successfully added!";
            return RedirectToAction("Index", "Home");
            
        }

        public ActionResult ContentById(int id)
        {
            var content = this.Data.Files.All().FirstOrDefault(x => x.Id == id).Content;
            
            return File(content, "audio/mp3");
        }
    }
}