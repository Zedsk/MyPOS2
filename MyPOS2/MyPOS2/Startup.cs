using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPOS2.Startup))]
namespace MyPOS2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
