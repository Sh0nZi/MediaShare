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
            var file = this.Data.Files.All().FirstOrDefault(x => x.Id == id);
            var content = file.Content;

            string contentType = "image/jpeg";
            content = file.Thumbnail;
            
            return this.File(content, contentType);
        }
    }
}