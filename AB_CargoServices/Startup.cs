using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AB_CargoServices.Startup))]
namespace AB_CargoServices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
