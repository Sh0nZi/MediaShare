using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MediaShare.Data;
using AutoMapper;
using System.Data.Entity;

namespace MediaShare.Web.Areas.Admin.Controllers
{
    public abstract class KendoGridAdministrationController : AdminController
    {
        public KendoGridAdministrationController(IMediaShareData data) : base(data)
        {
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [HttpPost]
        public ActionResult Read([DataSourceRequest]
                                 DataSourceRequest request)
        {
            var ads =
                this.GetData()
                    .ToDataSourceResult(request);

            return this.Json(ads);
        }

        [NonAction]
        protected virtual void Delete<TModel, TViewModel>(TViewModel model, object id)
            where TModel: class
             where TViewModel : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(id);
                var entry = this.Data.Context.Entry(dbModel);
                entry.State = EntityState.Deleted;
                this.Data.SaveChanges();
                Mapper.Map<TViewModel, TModel>(model, dbModel);
                
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