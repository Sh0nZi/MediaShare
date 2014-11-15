using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MediaShare.Data;
using AutoMapper;
using System.Data.Entity;
using MediaShare.Web.Areas.Admin.Models;
using System.Security.Principal;

namespace MediaShare.Web.Areas.Admin.Controllers
{
    public abstract class KendoGridAdministrationController : AdminController
    {
        public KendoGridAdministrationController(IMediaShareData data, IIdentity identity) : base(data,identity)
        {
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        public ActionResult Read([DataSourceRequest]
                                 DataSourceRequest request)
        {
            var ads =
                this.GetData()
                    .ToDataSourceResult(request);

            return this.Json(ads, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public virtual void Delete<TModel, TViewModel>(TViewModel model, object id)
            where TModel : class
            where TViewModel : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(id);
                Mapper.Map<TViewModel, TModel>(model, dbModel);
                var entry = this.Data.Context.Entry(dbModel);
                entry.State = EntityState.Deleted;
                this.Data.SaveChanges();
            }
        }

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]
                                              DataSourceRequest request)
        {
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}