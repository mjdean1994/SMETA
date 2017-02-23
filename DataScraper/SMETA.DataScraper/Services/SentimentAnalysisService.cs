using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Configuration;
using SMETA.DataAccess.Models;
using System.Web.Script.Serialization;

namespace SMETA.DataScraper.Services
{
    public static class SentimentAnalysisService
    {
        public static SentimentScore GetSentiment(string tweetBody)
        {
            WebRequest request = WebRequest.Create(WebConfigurationManager.AppSettings["SentimentAnalysis_BaseUri"] + tweetBody.Replace(" ","%20"));
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Deserialize<SentimentScore>(reader.ReadToEnd().Replace("\n",""));
        }
    }
}