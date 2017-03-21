using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SMETA.DataScraper.Services;

namespace SMETA.DataScraper.Tests.Services
{
    [TestClass]
    public class SentimentAnalysisServiceTest
    {
        [TestMethod]
        public void ConnectionStringTest()
        {
            var response = SentimentAnalysisService.GetSentiment("I'm so happy today!");
            
            Assert.IsNotNull(response);
        }
    }
}
