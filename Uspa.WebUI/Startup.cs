using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Uspa.WebUI.Startup))]
namespace Uspa.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
