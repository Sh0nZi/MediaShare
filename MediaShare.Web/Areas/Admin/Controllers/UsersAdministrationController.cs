namespace MediaShare.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;
    using MediaShare.Data;
    using MediaShare.Models;
    using MediaShare.Web.Areas.Admin.Models;
    using AutoMapper;

    public class UsersAdministrationController : KendoGridAdministrationController
    {
        public UsersAdministrationController(IMediaShareData data) : base(data)
        {
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Users.Find(id) as T;
        }

        // GET: Admin/UsersAdministration
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Users.All().Project().To<UserAdminViewModel>();
        }

        [HttpPost]
        public ActionResult Ban([DataSourceRequest]
                                DataSourceRequest request, UserAdminViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.GetById<ApplicationUser>(model.Id);
                Mapper.Map<UserAdminViewModel, ApplicationUser>(model, dbModel);
                var entry = this.Data.Context.Entry(dbModel);
                entry.Entity.IsBanned = model.IsBanned;
                entry.State = EntityState.Modified;
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}