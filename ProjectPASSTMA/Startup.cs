using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectPASSTMA.Startup))]
namespace ProjectPASSTMA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
