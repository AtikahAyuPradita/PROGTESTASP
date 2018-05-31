using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestBooksMVC.Startup))]
namespace TestBooksMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
