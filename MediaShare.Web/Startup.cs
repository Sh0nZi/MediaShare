using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MediaShare.Web.Startup))]
namespace MediaShare.Web
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
