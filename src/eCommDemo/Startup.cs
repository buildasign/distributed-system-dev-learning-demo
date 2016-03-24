using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eCommDemo.Startup))]
namespace eCommDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
