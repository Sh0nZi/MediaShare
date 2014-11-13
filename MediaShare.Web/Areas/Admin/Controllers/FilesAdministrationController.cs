namespace MediaShare.Web.Areas.Admin.Controllers
{
    using System;
    using System.Web.Mvc;
    using MediaShare.Data;
    using System.Collections;
    using Kendo.Mvc.UI;
    using MediaShare.Models;
    using MediaShare.Web.Models.Files;
    using AutoMapper.QueryableExtensions;

    public class FilesAdministrationController : KendoGridAdministrationController
    {
        public FilesAdministrationController(IMediaShareData data) : base(data)
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
            return this.MediaFiles.Project().To<AdvancedMediaFileViewModel>();
        }


        [HttpPost]
        public ActionResult Delete([DataSourceRequest]
                                   DataSourceRequest request, AdvancedMediaFileViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                base.Delete<MediaFile, AdvancedMediaFileViewModel>(model, model.Id);
            }

            return this.GridOperation(model, request);
        }
    }
}