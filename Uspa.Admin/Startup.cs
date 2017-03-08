using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Uspa.Admin.Startup))]
namespace Uspa.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
