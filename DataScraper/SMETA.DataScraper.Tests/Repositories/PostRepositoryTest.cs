using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SMETA.DataScraper;
using SMETA.DataAccess;
using MongoDB.Driver;
using System.Security.Authentication;

namespace SMETA.DataScraper.Tests.Repositories
{
    [TestClass]
    public class PostRepositoryTest
    {
        [TestMethod]
        public void ConnectionStringTest()
        {
            MongoClient client;
            IMongoDatabase database;

            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(Configuration.POST_DB_CONNECTION_STRING)
            );
            settings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            client = new MongoClient(settings);
            database = client.GetDatabase(Configuration.POST_DB_NAME);

            Assert.IsNotNull(database);
        }
    }
}
