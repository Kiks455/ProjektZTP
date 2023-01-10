using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjektZTP.Startup))]
namespace ProjektZTP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
