namespace MediaShare.Web.Areas.Authorized.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using PagedList;
    using AutoMapper.QueryableExtensions;

    using MediaShare.Data;
    using MediaShare.Web.Models.Files;

    public class FavouritesController : AuthorizedController
    {
        public FavouritesController(IMediaShareData data)
            : base(data)
        {

        }
        // GET: Authorized/Favourites
        public ActionResult Index(int? page)
        {
            var videos = this.Data.Users.Find(this.CurrentUser)
                            .Favourites.AsQueryable().Project().To<MediaFileViewModel>()
                            .OrderByDescending(f => f.DateCreated);
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(videos.ToPagedList(pageNumber, pageSize));
        }
    }
}