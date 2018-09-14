using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MobileCoverageMapApp.Startup))]
namespace MobileCoverageMapApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
