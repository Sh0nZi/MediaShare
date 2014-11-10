using MediaShare.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using MediaShare.Models;

namespace MediaShare.Web.Controllers
{
    public class AllFilesController : BaseController
    {

        public AllFilesController(IMediaShareData data)
            : base(data)
        {

        }

        // GET: AllFiles
        public ActionResult Index(int? page, string searchString, string type)
        {
 
            var videos = this.MediaFiles;

            if (!String.IsNullOrEmpty(searchString))
            {
                videos = videos.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(type))
            {
                if (type == "Audio")
                {
                    videos = videos.Where(s => s.Type == MediaType.Audio);
                }
                else
                {
                    videos = videos.Where(s => s.Type == MediaType.Video);
                }
            }
           

            videos = videos.OrderByDescending(f => f.DateCreated);
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PagedFiles", videos.ToPagedList(pageNumber, pageSize));
            }
            return View(videos.ToPagedList(pageNumber, pageSize));

        }

    }
}