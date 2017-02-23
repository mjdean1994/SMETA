using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SMETA.SentimentAnalysisPlatform.API.Startup))]

namespace SMETA.SentimentAnalysisPlatform.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
