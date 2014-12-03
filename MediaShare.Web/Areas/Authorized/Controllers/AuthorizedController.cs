namespace MediaShare.Web.Areas.Authorized.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Security.Principal;
    using Microsoft.AspNet.Identity;   

    using AutoMapper.QueryableExtensions;

    using MediaShare.Web.Controllers;
    using MediaShare.Data;
    using MediaShare.Web.Areas.Authorized.Models;
    
    [Authorize]
    public abstract class AuthorizedController : BaseController
    {
        protected const int PageSize = 3;

        private readonly IIdentity identity;

        public AuthorizedController(IMediaShareData data, IIdentity identity) : base(data)
        {
            this.identity = identity;
        }

        protected UserViewModel GetCurrentUser(string id = null)
        {
            var userId = string.IsNullOrEmpty(id) ? identity.GetUserId() : id;
            return this.Data.Users
                       .All().Project().To<UserViewModel>().AsQueryable()
                       .FirstOrDefault(u => u.Id == userId);            
        }
    }
}