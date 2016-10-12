using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;

namespace SMETA.DataScraper.Hubs
{
    public class TweetHub : Hub
    {
        public void PostTweet(string name, string handle, string message)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TweetHub>();
            context.Clients.All.broadcastMessage(name, handle, message);
        }
    }
}