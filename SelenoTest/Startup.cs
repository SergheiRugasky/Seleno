using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SelenoTest.Startup))]
namespace SelenoTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
