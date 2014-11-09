using MediaShare.Data;
using MediaShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaShare.Web.Controllers
{
    public class LatestFilesController : BaseController
    {
        public LatestFilesController(IMediaShareData data) : base(data)
        {

        }
        // GET: LatestFiles
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LatestVideo()
        {
            var videoFiles = this.MediaFiles
                .Where(f => f.Type == MediaType.Video)
                .Take(6).OrderByDescending(f=>f.DateCreated)
                .ToList();
            return PartialView("LatestPartial",videoFiles);
        }

        public ActionResult LatestAudio()
        {
            var audioFiles = this.MediaFiles
                .Where(f => f.Type == MediaType.Audio)
                .Take(6).OrderByDescending(f=>f.DateCreated)
                .ToList();
            return PartialView("LatestPartial",audioFiles);
        }
    }
}