namespace MediaShare.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using MediaShare.Web.Models.Files;
    using MediaShare.Data;
    using MediaShare.Models;

    public class HomeController : BaseController
    {
        private const int ShowTopNumber = 6;

        public HomeController(IMediaShareData data) : base(data)
        {
        }
             
        public ActionResult Index()
        {
            return this.View();
        }
        
        public ActionResult GetTopVideo()
        {
            var topVideo = this.MediaFiles.Project().To<BasicMediaFileViewModel>()
                               .Where(f => f.Type == MediaType.Video)
                               .OrderByDescending(f => (double)f.Votes.Sum(v => v.Value) / f.Votes.Count)
                               .ThenBy(f => f.Votes.Count)
                               .Take(ShowTopNumber)
                               .ToList();
            return this.PartialView("HomePartial", topVideo);
        }

        public ActionResult GetTopAudio()
        {
            var topAudio = this.MediaFiles.Project().To<BasicMediaFileViewModel>()
                               .Where(f => f.Type == MediaType.Audio)
                               .OrderByDescending(f => (double)f.Votes.Sum(v => v.Value) / f.Votes.Count)
                               .ThenBy(f => f.Votes.Count).Take(ShowTopNumber)
                               .ToList();
            return this.PartialView("HomePartial", topAudio);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}