namespace MediaShare.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using MediaShare.Data;
    using MediaShare.Models;
    using MediaShare.Web.Infrastructure.Helpers;

    [HandleError]
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
    }
}