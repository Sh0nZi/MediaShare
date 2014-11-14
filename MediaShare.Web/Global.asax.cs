namespace MediaShare.Web
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using MediaShare.Web.App_Start;
    using MediaShare.Web.Infrastructure.Mapping;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ViewEnginesConfig.RegisterEngines();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.Execute();
        }
    }
}