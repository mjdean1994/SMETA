using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SMETA.DataScraper.Services;

namespace SMETA.DataScraper.Tests.Services
{
    [TestClass]
    public class SentimentAnalysisServiceTest
    {
        [TestMethod]
        public void SentimentAnalysisApiTest()
        {
            var response = SentimentAnalysisService.GetSentiment("I'm so happy today!");
            
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void SentimentAnalysisAccuracyTest()
        {
            var response = SentimentAnalysisService.GetSentiment("I'm so happy today!");

            Assert.IsTrue(response.probability.pos > response.probability.neg);
        }
    }
}
