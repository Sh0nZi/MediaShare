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

namespace MediaShare.Web.Controllers
{
    [Authorize]
    public class UploadController : BaseController
    {
        
         public UploadController(IMediaShareData data) : base(data)
        {
        }
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload(MediaFile file, HttpPostedFileBase MediaFile)
        {
            var currentUser = Thread.CurrentPrincipal.Identity.GetUserId();
            if (MediaFile != null || currentUser != null)
            {
                file.Content = new byte[MediaFile.ContentLength];
                MediaFile.InputStream.Read(file.Content, 0, MediaFile.ContentLength);
                if (MediaFile.ContentType == "audio/mp3")
                {
                    file.Type = MediaType.Audio;
                    file.Thumbnail = this.GetAudioThumbnail();
                }
                else if (MediaFile.ContentType == "video/mp4")
                {
                    file.Type = MediaType.Video;
                    file.Thumbnail = this.GetVideoThumbnail(file.Content);
                }
                else
                { 
                    return RedirectToAction("Index");
                }
                
                file.DateCreated = DateTime.Now;
                file.AuthorId = currentUser;
               
                this.Data.Files.Add(file);
                this.Data.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
                
            return RedirectToAction("Index");
        }

        private byte[] GetVideoThumbnail(byte[] content)
        {
            string path = HttpRuntime.AppDomainAppPath + "\\MediaFiles\\sample.mp4";
            
            System.IO.File.WriteAllBytes(path, content);
            
            MemoryStream stream = new MemoryStream();
            (new FFMpegConverter()).GetVideoThumbnail(path, stream, 100);
            System.IO.File.Delete(path);
            return stream.ToArray();
        }
        
        private byte[] GetAudioThumbnail()
        {
            string path = HttpRuntime.AppDomainAppPath + "\\MediaFiles\\mp3.jpg";
            var content = System.IO.File.ReadAllBytes(path);
            return content.ToArray();
        }
    }
}