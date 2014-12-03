namespace MediaShare.Web.Areas.Authorized.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Security.Principal;
    using AutoMapper;
    using MediaShare.Data;
    using MediaShare.Models;
    using MediaShare.Web.Infrastructure.Helpers;
    using MediaShare.Web.Models.Files;
    
    [ValidateInput(false)]
    public class UploadFileController : AuthorizedController
    {
        private const string ContentRequiredText = "Content is required";

        private const string TooLargeFileText = "Your file is too large! Allowed size 50 mb";

        private const string WrongFormatText = "Your file is in wrong format! Allowed format - ";

        private const int MaxSize = 500000000;

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
        public ActionResult UploadVideo(MediaFileViewModel file, HttpPostedFileBase mediaFile)
        {

            if (!this.IsValid(mediaFile, "video/mp4") && !this.IsValid(mediaFile, "video/webm"))
            {
                return this.View("VideoIndex", file);
            }
            var dbFile = Mapper.Map<MediaFile>(file);
            this.PopulateContent(dbFile, mediaFile);
            dbFile.Type = MediaType.Video;
             
            this.Data.Files.Add(dbFile);
            this.Data.SaveChanges();

            this.TempData["Success"] = "Video successfully added!";
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
        
        // Post: Audio
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAudio(MediaFileViewModel file, HttpPostedFileBase mediaFile)
        {
            if (!this.IsValid(mediaFile, "audio/mp3") && !this.IsValid(mediaFile, "audio/mpeg"))
            {
                return this.View("AudioIndex", file);
            }
            var dbFile = Mapper.Map<MediaFile>(file);
            this.PopulateContent(dbFile, mediaFile);
            dbFile.Type = MediaType.Audio;
            
            this.Data.Files.Add(dbFile);
            this.Data.SaveChanges();
            
            this.TempData["Success"] = "Audio successfully added!";
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
                
        private void PopulateContent(MediaFile file, HttpPostedFileBase mediaFile)
        { 
            var fileName = RandomStringGenerator.GenerateString();
            if (mediaFile.ContentType == "audio/mp3" || mediaFile.ContentType == "audio/mpeg")
            {
                fileName += ".mp3";
            }
            else
            {
                fileName += ".mp4";
            }
            var fileContent = new byte[mediaFile.ContentLength]; 

            mediaFile.InputStream.Read(fileContent, 0, mediaFile.ContentLength);
            DropboxHandler.UploadFile(fileContent, fileName);  

            if (mediaFile.ContentType == "audio/mp3" || mediaFile.ContentType == "audio/mpeg")
            {
                fileContent = null;
            }

            var fileThumbnail = thumbnailExtractor.GetThumbnail(fileContent);                    
            DropboxHandler.UploadFile(fileThumbnail, fileName + "thumb.jpeg");

            file.Content = fileName;
            file.Thumbnail = fileName + "thumb.jpeg";
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