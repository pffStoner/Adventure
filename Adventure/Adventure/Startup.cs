using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Adventure.Startup))]
namespace Adventure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
