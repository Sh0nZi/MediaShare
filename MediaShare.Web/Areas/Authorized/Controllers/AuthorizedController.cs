namespace MediaShare.Web.Areas.Authorized.Controllers
{
    using System.Web.Mvc;
    using MediaShare.Web.Controllers;
    using Microsoft.AspNet.Identity;
    using MediaShare.Data;
    
    [Authorize]
    public abstract class AuthorizedController : BaseController
    {
        public AuthorizedController(IMediaShareData data) : base(data)
        {
        }
     
        protected string CurrentUser
        {
            get
            {
                return this.User.Identity.GetUserId();
            }
        }
    }
}