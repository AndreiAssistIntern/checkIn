using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(andrei3.Startup))]
namespace andrei3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
