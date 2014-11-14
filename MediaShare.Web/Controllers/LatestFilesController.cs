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
            return this.View();
        }

        public ActionResult LatestVideo()
        {
            var videoFiles = this.MediaFiles.Project().To<BasicMediaFileViewModel>()
                                 .Where(f => f.Type == MediaType.Video)
                                 .OrderByDescending(f => f.DateCreated)
                                 .Take(6)
                                 .ToList();
            return this.PartialView("LatestPartial", videoFiles);
        }

        public ActionResult LatestAudio()
        {
            var audioFiles = this.MediaFiles.Project().To<BasicMediaFileViewModel>()
                                 .Where(f => f.Type == MediaType.Audio)
                                 .OrderByDescending(f => f.DateCreated)
                                 .Take(6)
                                 .ToList();
            return this.PartialView("LatestPartial", audioFiles);
        }
    }
}