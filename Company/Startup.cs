using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Company.Startup))]
namespace Company
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
