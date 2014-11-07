using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MediaShare.Data;
using MediaShare.Models;
using MediaShare.Web.Helpers;

namespace MediaShare.Web.Areas.Authorized.Controllers
{
    public class UploadFileController : AuthorizedController
    {
        public UploadFileController(IMediaShareData data) : base(data)
        {
        }

        // GET: UploadVideoFile
        public ActionResult VideoIndex()
        {
            return this.View();
        }

        // GET: UploadAudioFile
        public ActionResult AudioIndex()
        {
            return this.View();
        }

        // Post: Video
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult UploadVideo(MediaFile file, HttpPostedFileBase mediaFile)
        {
            if (mediaFile == null)
            {
                this.ModelState.AddModelError("Content", "Content is required");
                return this.View("VideoIndex", file);
            }

            if (mediaFile != null && mediaFile.ContentLength > 50000000)
            {
                this.ModelState.AddModelError("Size", "Your file is too large! Allowed size 50 mb");
                return this.View("VideoIndex", file);
            }
            if (mediaFile != null && mediaFile.ContentType != "video/mp4")
            {
                this.ModelState.AddModelError("Type", "Your file is in wrong format! Allowed format - mp4");
                return this.View("VideoIndex", file);
            }

            this.PopulateContent(file, mediaFile);
            file.Thumbnail = ThumbnailExtractor.GetVideoThumbnail(file.Content);
            file.Type = MediaType.Video;
            //Entity Framework does not allow to store files larger than 50mb...
             
            this.Data.Files.Add(file);
            this.Data.SaveChanges();
            this.TempData["Success"] = "Video successfully added!";
            return this.RedirectToAction("Index", "Home",new {area=""});
        }
        
        // Post: Audio
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAudio(MediaFile file, HttpPostedFileBase mediaFile)
        {
            if (mediaFile == null)
            {
                this.ModelState.AddModelError("Content", "Content is required");
                return this.View("AudioIndex", file);
            }

            if (mediaFile.ContentLength > 50000000)
            {
                this.ModelState.AddModelError("Size", "Your file is too large! Allowed size 50 mb");
                return this.View("AudioIndex", file);
            }
            if (mediaFile.ContentType != "audio/mp3")
            {
                this.ModelState.AddModelError("Type", "Your file is in wrong format! Allowed format - mp3");
                return this.View("AudioIndex", file);
            }

            this.PopulateContent(file, mediaFile);
            file.Type = MediaType.Audio;
            file.Thumbnail = ThumbnailExtractor.GetAudioThumbnail();
            
            this.Data.Files.Add(file);
            this.Data.SaveChanges();
            
            this.TempData["Success"] = "Audio successfully added!";
            return this.RedirectToAction("Index", "Home", new {area=""} );
        }
                
        private void PopulateContent(MediaFile file, HttpPostedFileBase mediaFile)
        {
            file.Content = new byte[mediaFile.ContentLength];              
            mediaFile.InputStream.Read(file.Content, 0, mediaFile.ContentLength);
            file.DateCreated = DateTime.Now;
            file.AuthorId = this.CurrentUser;
        }
    }
}