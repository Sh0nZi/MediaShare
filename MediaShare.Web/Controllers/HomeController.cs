namespace MediaShare.Web.Controllers
{
    using System.Collections.Generic;
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
            var topItems = this.MediaFiles.Project().To<BasicMediaFileViewModel>();
            return this.View(topItems);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}