using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TirabosqueDesireeTesis.Startup))]
namespace TirabosqueDesireeTesis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
