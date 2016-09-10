using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BitBookWebApp.Startup))]
namespace BitBookWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
