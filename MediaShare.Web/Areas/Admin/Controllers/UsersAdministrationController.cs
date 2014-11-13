namespace MediaShare.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections;
    using System.Web.Mvc;
    using MediaShare.Data;

    public class UsersAdministrationController : KendoGridAdministrationController
    {
        public UsersAdministrationController(IMediaShareData data) : base(data)
        {
        }

        protected override T GetById<T>(object id)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        // GET: Admin/UsersAdministration
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            throw new System.NotImplementedException();
        }
    }
}