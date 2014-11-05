using MediaShare.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaShare.Web.Controllers
{
    public class MediaFileDetailsController : BaseController
    {
        public MediaFileDetailsController(IMediaShareData data) : base(data)
        {
        }

        // GET: MediaFileDetails
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var file = this.Data.Files.All().FirstOrDefault(f => f.Id == id);
            return View(file);
        }
    }
}