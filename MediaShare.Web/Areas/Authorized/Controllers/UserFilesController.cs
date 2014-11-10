using MediaShare.Data;
using MediaShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace MediaShare.Web.Areas.Authorized.Controllers
{
    public class UserFilesController : AuthorizedController
    {
     
        public UserFilesController(IMediaShareData data)
            : base(data)
        {
           
        }
        // GET: Authorized/UserFiles
        public ActionResult Index(string id, int? page)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            var userName = this.Data.Users.Find(id).UserName;
            if (id == this.CurrentUser)
            {
                ViewBag.UserName = "Your";
            }
            else
            {
                ViewBag.UserName = userName.Substring(0, userName.IndexOf('@')) + " 's";

            }

            var videos = this.MediaFiles
                               .Where(f => f.AuthorId == id).OrderByDescending(f => f.DateCreated);
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(videos.ToPagedList(pageNumber, pageSize));
        }

    }
}