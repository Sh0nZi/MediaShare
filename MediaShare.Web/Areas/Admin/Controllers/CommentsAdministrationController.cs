namespace MediaShare.Web.Areas.Admin.Controllers
{
    using System.Collections;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using MediaShare.Data;
    using MediaShare.Web.Models;
    using Kendo.Mvc.UI;
    using MediaShare.Models;
    using System.Security.Principal;

    public class CommentsAdministrationController : KendoGridAdministrationController
    {
        public CommentsAdministrationController(IMediaShareData data, IIdentity identity) : base(data,identity)
        {
        }

        protected override T GetById<T>(object id)
        {
            // TODO: Implement this method
            return this.Data.Comments.Find(id) as T;
        }

        // GET: Admin/CommentsAdministration
        public ActionResult Index()
        {
            return this.View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Comments.All().Project().To<CommentViewModel>();
        }
        
        [HttpPost]
        public ActionResult Delete([DataSourceRequest]
                                   DataSourceRequest request, CommentViewModel model)
        {
            base.Delete<Comment, CommentViewModel>(model, model.Id);

            return this.GridOperation(model, request);
        }
    }
}