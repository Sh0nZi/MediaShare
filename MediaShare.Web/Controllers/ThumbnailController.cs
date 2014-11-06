using MediaShare.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaShare.Web.Controllers
{
    public class ThumbnailController : BaseController
    {
        public ThumbnailController(IMediaShareData data) : base(data)
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
    }
}