using MediaShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaShare.Web.Models;
using MediaShare.Data;

namespace MediaShare.Web.Controllers
{
    public class MediaFilesController : BaseController
    {
        public MediaFilesController(IMediaShareData data) : base(data)
        {
        }

        public ActionResult ByIdThumbnail(int id)
        {
            var file = this.Data.Files.All().FirstOrDefault(x => x.Id == id);
            var content = file.Content;

            string contentType = "image/jpeg";
            content = file.Thumbnail;
            
            return File(content, contentType);
        }

        public ActionResult ByIdContent(int id)
        {
            var file = this.Data.Files.All().FirstOrDefault(x => x.Id == id);
            var content = file.Content;

            string contentType = "";

            if (file.Type == MediaType.Video)
            { 
                contentType = "video/mp4";
            }
            else if (file.Type == MediaType.Audio)
            {
                contentType = "audio/mp3";
            }

            return File(content, contentType);
        }
    }
}