using MediaShare.Data;
using MediaShare.Models;
using MediaShare.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaShare.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IMediaShareData data) : base(data)
        {
        }

        public ActionResult Index()
        {
            HomeViewModel homeModel = new HomeViewModel();
            homeModel.Top3VideoFiles = this.Data.Files.All()
                                           .Where(f => f.Type == MediaType.Video)
                                           .OrderByDescending(f => f.Votes.Sum(v => v.Value) / f.Votes.Count)
                                           .ThenBy(f => f.Votes.Count).Take(3)
                                           .ToList();
            homeModel.Top3AudioFiles = this.Data.Files.All()
                                           .Where(f => f.Type == MediaType.Audio)
                                           .OrderByDescending(f => f.Votes.Sum(v => v.Value) / f.Votes.Count)
                                           .ThenBy(f => f.Votes.Count).Take(3)
                                           .ToList();
            return View(homeModel);
        }
    }
}