﻿using MediaShare.Data;
using MediaShare.Web.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaShare.Web.Controllers
{
    public class ThumbnailController : BaseController
    {
        public ThumbnailController(IMediaShareData data) : base(data)
        {
        }

        // GET: Thumbnail
        public ActionResult Index(int id)
        {
            var file = this.MediaFiles.FirstOrDefault(x => x.Id == id);
            ViewBag.ThumbLink = DropboxHandler.GetUrl(file.Thumbnail);
            return PartialView(id);
        }
    }
}