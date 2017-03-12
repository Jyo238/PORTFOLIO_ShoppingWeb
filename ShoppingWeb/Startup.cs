using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShoppingWeb.Startup))]
namespace ShoppingWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
