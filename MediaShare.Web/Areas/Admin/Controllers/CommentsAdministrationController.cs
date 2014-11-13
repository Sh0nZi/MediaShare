namespace MediaShare.Web.Areas.Admin.Controllers
{
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;

    using MediaShare.Data;
    using System;

    public class CommentsAdministrationController : KendoGridAdministrationController
    {
        public CommentsAdministrationController(IMediaShareData data) : base(data)
        {
        }

        protected override T GetById<T>(object id)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        // GET: Admin/CommentsAdministration
        public ActionResult Index()
        {
            return this.View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Comments.All();
        }
    }
}