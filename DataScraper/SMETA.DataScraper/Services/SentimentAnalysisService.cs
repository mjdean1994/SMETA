using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Configuration;
using SMETA.DataAccess.Models;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Text;

namespace SMETA.DataScraper.Services
{
    public static class SentimentAnalysisService
    {
        public static SentimentScore GetSentiment(string tweetBody)
        {
            StringBuilder postData = new StringBuilder();
            postData.Append(String.Format("text={0}", HttpUtility.UrlEncode(tweetBody)));

            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] postBytes = ascii.GetBytes(postData.ToString());

            String baseUri = WebConfigurationManager.AppSettings["SentimentAnalysis_BaseUri"];

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(baseUri);
            request.Method = "POST";
            request.Headers["X-Mashape-Key"] = "qCsHwhmVhZmshnWWPWRaOkfTm8U3p1sEfzkjsnWrWnhGHpbxU9";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

            Stream postStream = request.GetRequestStream();
            postStream.Write(postBytes, 0, postBytes.Length);
            postStream.Flush();
            postStream.Close();

            WebResponse response = request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                string jsonString = sr.ReadToEnd();

                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                SentimentScore score = jsonSerializer.Deserialize<SentimentScore>(jsonString);

                return score;
            }
        }
    }
}