using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DairyFarm.Web.Startup))]
namespace DairyFarm.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
