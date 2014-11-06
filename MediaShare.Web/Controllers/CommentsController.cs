using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MediaShare.Web.Models;
using Microsoft.AspNet.Identity;
using MediaShare.Models;
using MediaShare.Data;

namespace MediaShare.Web.Controllers
{
    public class CommentsController : BaseController
    {
        public CommentsController(IMediaShareData data) : base(data)
        {
        }

        [Authorize]
        // POST: Comment       
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Post(Comment comment, int id)
        {
            var currentUser = this.CurrentUser;

            comment.DateCreated = DateTime.Now;
            comment.AuthorId = currentUser;

            var file = this.Data.Files.All().FirstOrDefault(f => f.Id == id);
            file.Comments.Add(comment);

            this.Data.SaveChanges();
            
            return RedirectToAction("FileComments", new { Id = id });
        }
        
        public ActionResult FileComments(int id)
        {
            var comments = this.Data.Files.All().FirstOrDefault(f => f.Id == id).Comments
                               .OrderByDescending(c => c.DateCreated)
                               .AsQueryable().Select(CommentViewModel.FromComment)
                               .ToList();           
            return PartialView(comments);
        }
    }
}