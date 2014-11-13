namespace MediaShare.Web.Areas.Authorized.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using AutoMapper.QueryableExtensions;
    using MediaShare.Web.Controllers;
    using MediaShare.Data;
    using MediaShare.Web.Areas.Authorized.Models;
    
    [Authorize]
    public abstract class AuthorizedController : BaseController
    {
        protected const int PageSize = 3;

        public AuthorizedController(IMediaShareData data) : base(data)
        {
        }

        protected UserViewModel GetCurrentUser(string id = null)
        {
            var userId=string.IsNullOrEmpty(id)?this.User.Identity.GetUserId():id;
            return this.Data.Users
                       .All().Project().To<UserViewModel>().AsQueryable()
                       .FirstOrDefault(u => u.Id == userId);            
        }
    }
}