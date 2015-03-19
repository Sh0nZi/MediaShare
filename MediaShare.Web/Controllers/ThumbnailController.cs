using System.Security.Principal;
using MediaShare.Data;
using MediaShare.Web.Areas.Authorized.Controllers;
using MediaShare.Web.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaShare.Models;

namespace MediaShare.Web.Controllers
{
    public class ThumbnailController : BaseController
    {
        public ThumbnailController(IMediaShareData data) : base(data)
        {
        }
        
        // GET: Thumbnail
        public ActionResult Index(int id)
        {
            var file = this.MediaFiles.FirstOrDefault(x => x.Id == id);
            if (file.Type == MediaType.Video)
            {
                ViewBag.ThumbLink = DropboxHandler.GetUrl(file.Thumbnail);
            }
            else
            {
                 ViewBag.ThumbLink = DropboxHandler.GetUrl("FYGuzop.mp4thumb.jpeg");
            }
            return PartialView(id);
        }
    }
}