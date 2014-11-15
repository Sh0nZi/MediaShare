using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MediaShare.Web.App_Start.Startup))]
namespace MediaShare.Web.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
