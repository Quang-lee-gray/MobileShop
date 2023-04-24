using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MobileShop.Startup))]
namespace MobileShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
