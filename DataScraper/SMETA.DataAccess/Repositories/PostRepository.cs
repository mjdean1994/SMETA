using MongoDB.Driver;
using System.Collections.Generic;
using System.Security.Authentication;
using SMETA.DataAccess.Models;
using Tweetinvi.Models;
using System;
using MongoDB.Bson;

namespace SMETA.DataAccess.Repositories
{
    public class PostRepository
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        
        public PostRepository()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(Configuration.POST_DB_CONNECTION_STRING)
            );
            settings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            _client = new MongoClient(settings);
            _database = _client.GetDatabase(Configuration.POST_DB_NAME);
        }
        
        public List<Post> GetAllPosts()
        {
            IMongoCollection<Post> collection = _database.GetCollection<Post>("posts");
            return collection.Find(x => true).ToList();
        }

        public void InsertPosts(List<Post> posts)
        {
            IMongoCollection<Post> collection = _database.GetCollection<Post>("posts");
            collection.InsertMany(posts);
        }

        public Post GetSinglePost()
        {
            IMongoCollection<Post> collection = _database.GetCollection<Post>("posts");
            return collection.Find(x => true).FirstOrDefault();
        }

        //function to query based on search filter, change to List<Post>
        //Figure out where the queries are supposed to be
        public void GetSearchPost(String searchTerms)
        {
            IMongoCollection<Post> collection = _database.GetCollection<Post>("posts");
            //return collection.Find(searchTerms).FirstOrDefault();
        }
    }
}
