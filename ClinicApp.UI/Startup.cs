using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClinicApp.UI.Startup))]
namespace ClinicApp.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
