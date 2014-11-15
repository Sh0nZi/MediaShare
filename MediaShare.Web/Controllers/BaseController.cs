namespace MediaShare.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using MediaShare.Data;
    using MediaShare.Models;

    public class BaseController : Controller
    {
        public BaseController(IMediaShareData data)
        {
            this.Data = data;
        }
        
        protected IMediaShareData Data { get; private set; }

        protected IQueryable<MediaFile> MediaFiles
        {
            get
            {
                return this.Data.Files.All();
            }
        }

        public ActionResult ByIdThumbnail(int id)
        {
            var file = this.MediaFiles.FirstOrDefault(x => x.Id == id);

            string contentType = "image/jpeg";
            var content = file.Thumbnail;
            
            return this.File(content, contentType);
        }
    }
}