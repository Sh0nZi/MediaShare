using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MediaShare.Models;
using MediaShare.Data;

namespace MediaShare.Web.Controllers
{
    [Authorize]
    public class CommentsController : BaseController
    {
        public CommentsController(IMediaShareData data) : base(data)
        {
        }

        // POST: Comment
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Post(Comment comment, int id)
        {
            var currentUser = Thread.CurrentPrincipal.Identity.GetUserId();

            if (currentUser == null)
            {
                //ModelState.AddModelError("", "You must log in in order to comment!");
                //return View();
                return RedirectToAction("Index", "Home");
            }
            //else if (comment.Content.Length < 10 || comment.Content.Length > 300)
            //{
            //    return RedirectToAction("Details", "MediaFileDetails", new { Id = id });
            //}

            comment.DateCreated = DateTime.Now;
            comment.AuthorId = currentUser;

            var file = this.Data.Files.All().FirstOrDefault(f => f.Id == id);
            file.Comments.Add(comment);
            
            this.Data.SaveChanges();

            return RedirectToAction("Details", "MediaFileDetails", new { Id = id });
        }

        [ChildActionOnly]
        public ActionResult FileComments(int id)
        {
            var comments = this.Data.Files.All().FirstOrDefault(f => f.Id == id).Comments;
            return PartialView(comments);
        }
    }
}