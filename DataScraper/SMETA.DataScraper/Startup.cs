using Microsoft.Owin;
using Owin;
using SMETA.DataScraper.Services;

[assembly: OwinStartupAttribute(typeof(SMETA.DataScraper.Startup))]
namespace SMETA.DataScraper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            TwitterStreamingService.StartStream();
            app.MapSignalR();
        }
    }
}
