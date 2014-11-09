using MediaShare.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaShare.Web.Controllers
{
    public class AllFilesController : BaseController
    {
        public AllFilesController(IMediaShareData data) : base(data)
        {

        }

        // GET: AllFiles
        public ActionResult Index()
        {
            return View();
        }

    }
}