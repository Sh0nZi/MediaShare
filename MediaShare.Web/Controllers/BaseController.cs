using System;
using System.Linq;
using System.Web.Mvc;
using MediaShare.Data;
using Microsoft.AspNet.Identity;

namespace MediaShare.Web.Controllers
{
    public class BaseController : Controller
    {
        public IMediaShareData Data { get; private set; }

        protected string CurrentUser
        {
            get
            {
                return User.Identity.GetUserId();
            }
        }

        public BaseController(IMediaShareData data)
        {
            this.Data = data;
        }
    }
}