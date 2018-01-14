using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mboard.Startup))]
namespace mboard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
