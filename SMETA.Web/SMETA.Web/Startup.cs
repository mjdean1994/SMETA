using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMETA.Web.Startup))]
namespace SMETA.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
