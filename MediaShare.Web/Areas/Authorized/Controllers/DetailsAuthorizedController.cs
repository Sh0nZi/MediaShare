namespace MediaShare.Web.Areas.Authorized.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc; 
    using System.Security.Principal;

    using AutoMapper;

    using MediaShare.Data;
    using MediaShare.Models;
    using MediaShare.Web.Models;

    [ValidateInput(false)]
    public class DetailsAuthorizedController : AuthorizedController
    {
        public DetailsAuthorizedController(IMediaShareData data, IIdentity identity) : base(data,identity)
        {
        }
         
        // POST: Vote    
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(Vote vote, int id)
        {
            var file = this.MediaFiles.FirstOrDefault(f => f.Id == id);
            if (!file.Votes.Any(v => v.AuthorId == this.GetCurrentUser().Id) &&
                file.AuthorId != this.GetCurrentUser().Id)
            {
                file.Votes.Add(new Vote
                {
                    Value = vote.Value,
                    AuthorId = this.GetCurrentUser().Id
                });
                this.Data.SaveChanges();         
            }
          
            return this.RedirectToAction("FileVotes", "FileDetails", new { Id = id });
        }
        
        // POST: Comment       
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(CommentViewModel comment, int id)
        {
            var dbComment = Mapper.Map<Comment>(comment);
            dbComment.DateCreated = DateTime.Now;
            dbComment.AuthorId = this.GetCurrentUser().Id;

            var file = this.MediaFiles.FirstOrDefault(f => f.Id == id);
            file.Comments.Add(dbComment);

            this.Data.SaveChanges();
            
            return this.RedirectToAction("FileComments", "FileDetails", new { Id = id });
        }

        // POST: Favourites Add 
        [AcceptVerbs(HttpVerbs.Post)]
        public void AddFavourites(int id)
        {
            var file = this.MediaFiles.FirstOrDefault(f => f.Id == id);

            if (file.AuthorId == this.GetCurrentUser().Id || this.GetCurrentUser().Favourites.Any(f => f.Id == id))
            {
                return;
            }

            var userId = this.GetCurrentUser().Id.ToString();
            var user = this.Data.Users.Find(userId);
            user.Favourites.Add(file);   
        
            this.Data.SaveChanges();
        }

        // POST: Favourites Remove   
        [AcceptVerbs(HttpVerbs.Post)]
        public void RemoveFavourites(int id)
        {
            var file = this.MediaFiles.FirstOrDefault(f => f.Id == id);
            if (file.AuthorId == this.GetCurrentUser().Id)
            {
                return;
            }

            var userId = this.GetCurrentUser().Id.ToString();
            var user = this.Data.Users.Find(userId);
            user.Favourites.Remove(file); 
           
            this.Data.SaveChanges();
        }
    }
}