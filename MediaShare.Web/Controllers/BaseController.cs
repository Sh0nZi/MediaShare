using System;
using System.Linq;
using System.Web.Mvc;
using MediaShare.Data;
using Microsoft.AspNet.Identity;
using MediaShare.Models;

namespace MediaShare.Web.Controllers
{
    public class BaseController : Controller
    {
        public IMediaShareData Data { get; private set; }

        protected IQueryable<MediaFile> MediaFiles
        {
            get
            {
                return this.Data.Files.All();
            }
        }

        public BaseController(IMediaShareData data)
        {
            this.Data = data;
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