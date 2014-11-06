using MediaShare.Data;
using MediaShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaShare.Web.Controllers
{
    public class VotesController : BaseController
    {
        public VotesController(IMediaShareData data) : base(data)
        {
        }

        // GET: Votes
        public ActionResult Index(int id)
        {
            var file = this.Data.Files.All().FirstOrDefault(f => f.Id == id);
            return PartialView("RatingPartial", file);
        }

        // POST: Vote    
        [Authorize]   
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(Vote vote, int id)
        {
            var file = this.Data.Files.All().FirstOrDefault(f => f.Id == id);
            if(!file.Votes.Any(v => v.AuthorId == this.CurrentUser) &&
                file.AuthorId != this.CurrentUser)
            {
                    file.Votes.Add(new Vote
            {
                Value = vote.Value,
                AuthorId = this.CurrentUser
            });
            this.Data.SaveChanges();         
            }
          
            return RedirectToAction("Index", new { Id = id });
        }
    }
}