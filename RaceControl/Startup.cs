using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RaceControl.Startup))]
namespace RaceControl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
