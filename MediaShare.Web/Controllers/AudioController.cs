using MediaShare.Web.CustomResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaShare.Web.Controllers
{
    public class AudioController : Controller
    {
        // GET: Audio
        public ActionResult Index()
        {
            return new AudioResult();
        }
    }
}