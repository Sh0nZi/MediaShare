namespace MediaShare.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using System.Collections;

    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;

    using MediaShare.Models;
    using MediaShare.Data;
    using MediaShare.Web.Areas.Admin.Models;
    using System.Security.Principal;

    public class FilesAdministrationController : KendoGridAdministrationController
    {
        public FilesAdministrationController(IMediaShareData data, IIdentity identity) : base(data,identity)
        {
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Files.Find(id) as T;
        }

        // GET: Admin/Files
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.MediaFiles.Project().To<FileAdminViewModel>();
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest]
                                   DataSourceRequest request, FileAdminViewModel model)
        {
            base.Delete<MediaFile, FileAdminViewModel>(model, model.Id);

            return this.GridOperation(model, request);
        }
    }
}