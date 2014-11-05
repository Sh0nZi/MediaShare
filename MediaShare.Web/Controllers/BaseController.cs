using MediaShare.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaShare.Web.Controllers
{
    public class BaseController : Controller
    {
        public IMediaShareData Data { get; private set; }

        

        public BaseController(IMediaShareData data)
        {
            this.Data = data;
        }
    }
}