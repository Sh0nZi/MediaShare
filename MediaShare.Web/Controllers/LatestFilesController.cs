namespace MediaShare.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using MediaShare.Data;
    using MediaShare.Web.Models.Files;
    using MediaShare.Models;

    public class LatestFilesController : BaseController
    {
        public LatestFilesController(IMediaShareData data) : base(data)
        {
        }

        // GET: LatestFiles
        public ActionResult Index()
        {
            var files = this.MediaFiles.Project().To<BasicMediaFileViewModel>();
            return this.View(files);
        }
    }
}