namespace MediaShare.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using System.Security.Principal;

    using MediaShare.Common;
    using MediaShare.Data;
    using MediaShare.Web.Areas.Authorized.Controllers;

    [Authorize(Roles = GlobalConstants.Admin)]
    public class AdminController : AuthorizedController
    {
        public AdminController(IMediaShareData data, IIdentity identity) : base(data, identity)
        {
        }
    }
}