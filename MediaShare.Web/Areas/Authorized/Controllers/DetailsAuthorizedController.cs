namespace MediaShare.Web.Areas.Authorized.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using MediaShare.Data;
    using MediaShare.Models;

    public class DetailsAuthorizedController : AuthorizedController
    {
        public DetailsAuthorizedController(IMediaShareData data) : base(data)
        {
        }
         
        // POST: Vote    
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(Vote vote, int id)
        {
            var file = this.MediaFiles.FirstOrDefault(f => f.Id == id);
            if (!file.Votes.Any(v => v.AuthorId == this.CurrentUser) &&
                file.AuthorId != this.CurrentUser)
            {
                file.Votes.Add(new Vote
                {
                    Value = vote.Value,
                    AuthorId = this.CurrentUser
                });
                this.Data.SaveChanges();         
            }
          
            return this.RedirectToAction("FileVotes", "FileDetails", new { Id = id });
        }
        
        // POST: Comment       
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Post(Comment comment, int id)
        {
            comment.DateCreated = DateTime.Now;
            comment.AuthorId = this.CurrentUser;

            var file = this.MediaFiles.FirstOrDefault(f => f.Id == id);
            file.Comments.Add(comment);

            this.Data.SaveChanges();
            
            return this.RedirectToAction("FileComments", "FileDetails", new { Id = id });
        }

        // POST: Favourites Add 
        [AcceptVerbs(HttpVerbs.Post)]
        public void AddFavourites(int id)
        {
            var currentUser = this.Data.Users.All().FirstOrDefault(u => u.Id == this.CurrentUser);
            var file = this.MediaFiles.FirstOrDefault(f => f.Id == id);

            if (file.AuthorId == currentUser.Id)
            {
                return;
            }

            currentUser.Favourites.Add(file);           
            this.Data.SaveChanges();
        }

        // POST: Favourites Remove   
        [AcceptVerbs(HttpVerbs.Post)]
        public void RemoveFavourites(int id)
        {
            var currentUser = this.Data.Users.All().FirstOrDefault(u => u.Id == this.CurrentUser);
            var file = this.MediaFiles.FirstOrDefault(f => f.Id == id);

            if (file.AuthorId == currentUser.Id)
            {
                return;
            }

            currentUser.Favourites.Remove(file);            
            this.Data.SaveChanges();
        }
    }
}