using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdventureWorksMilestone2.Startup))]
namespace AdventureWorksMilestone2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
