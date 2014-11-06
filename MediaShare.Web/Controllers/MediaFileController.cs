using MediaShare.Data;
using MediaShare.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediaShare.Web.Helpers;

namespace MediaShare.Web.Controllers
{
    public class MediaFileController : BaseController
    {
        public MediaFileController(IMediaShareData data) : base(data)
        {
        }

        [Authorize]
        public ActionResult Upload()
        {
            return View();
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

        protected void PopulateContent(MediaFile file, HttpPostedFileBase mediaFile)
        {
            //VarbinaryStream blob = new VarbinaryStream("Data Source=.;Initial Catalog=MediaShare;Integrated Security=True",
            //    "MediaFiles","Content","Id",file.Id);
            //mediaFile.InputStream.CopyToAsync(blob, 8040);
            
            file.Content = new byte[mediaFile.ContentLength];              
            mediaFile.InputStream.Read(file.Content, 0, mediaFile.ContentLength);
            file.DateCreated = DateTime.Now;
            file.AuthorId = this.CurrentUser;
        }
    }
}