using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SBMSAdmin.Startup))]
namespace SBMSAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
