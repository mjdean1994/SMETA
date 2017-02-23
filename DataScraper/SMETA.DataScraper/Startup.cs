using Microsoft.Owin;
using Owin;
using SMETA.DataScraper.Services;
using SMETA.DataAccess.Repositories;

[assembly: OwinStartupAttribute(typeof(SMETA.DataScraper.Startup))]
namespace SMETA.DataScraper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
