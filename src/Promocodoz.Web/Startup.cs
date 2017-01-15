using Microsoft.Owin;
using Owin;
using Promocodoz.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Promocodoz.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
