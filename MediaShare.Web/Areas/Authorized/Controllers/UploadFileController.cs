namespace MediaShare.Web.Areas.Authorized.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MediaShare.Data;
    using MediaShare.Models;
    using MediaShare.Web.Infrastructure.Helpers;
using System.Security.Principal;

    public class UploadFileController : AuthorizedController
    {
        private const string ContentRequiredText = "Content is required";

        private const string TooLargeFileText = "Your file is too large! Allowed size 50 mb";

        private const string WrongFormatText = "Your file is in wrong format! Allowed format - ";

        private const int MaxSize = 50000000;

        private readonly IMediaHelper thumbnailExtractor;

        public UploadFileController(IMediaShareData data, IMediaHelper thumbnailExtractor, IIdentity identity) : base(data,identity)
        {
            this.thumbnailExtractor = thumbnailExtractor;
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
            if (!this.IsValid(mediaFile, "video/mp4") && !this.IsValid(mediaFile, "video/webm"))
            {
                return this.View("VideoIndex", file);
            }

            this.PopulateContent(file, mediaFile);
            file.Thumbnail = thumbnailExtractor.GetVideoThumbnail(file.Content);
            file.Type = MediaType.Video;
            //Entity Framework does not allow to store files larger than 50mb...
             
            this.Data.Files.Add(file);
            this.Data.SaveChanges();

            this.TempData["Success"] = "Video successfully added!";
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
        
        // Post: Audio
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAudio(MediaFile file, HttpPostedFileBase mediaFile)
        {
            if (!this.IsValid(mediaFile, "audio/mp3"))
            {
                return this.View("AudioIndex", file);
            }

            this.PopulateContent(file, mediaFile);
            file.Type = MediaType.Audio;
            file.Thumbnail = thumbnailExtractor.GetAudioThumbnail();
            
            this.Data.Files.Add(file);
            this.Data.SaveChanges();
            
            this.TempData["Success"] = "Audio successfully added!";
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
                
        private void PopulateContent(MediaFile file, HttpPostedFileBase mediaFile)
        { 
            file.Content= new byte[mediaFile.ContentLength]; 
            mediaFile.InputStream.Read( file.Content, 0, mediaFile.ContentLength);
           
            file.DateCreated = DateTime.Now;
            file.AuthorId = this.GetCurrentUser().Id;
        }

        private bool IsValid(HttpPostedFileBase mediaFile, string contentType)
        {
            if (mediaFile == null)
            {
                this.ModelState.AddModelError("Content", ContentRequiredText);
                return false;
            }
            if (mediaFile != null && mediaFile.ContentLength > MaxSize)
            {
                this.ModelState.AddModelError("Size", TooLargeFileText);
                return false;
            }
            if (mediaFile != null && mediaFile.ContentType != contentType)
            {
                this.ModelState.AddModelError("Type", WrongFormatText + contentType);
                return false;
            }
            return true;
        }
    }
}