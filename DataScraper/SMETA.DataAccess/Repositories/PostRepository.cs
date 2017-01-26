using MongoDB.Driver;
using System.Collections.Generic;
using System.Security.Authentication;
using SMETA.DataAccess.Models;
using Tweetinvi.Models;

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

        /// <summary>
        /// Returns all tweets submitted by the given username
        /// </summary>
        /// <param name="username">A twitter handle to search the database for</param>
        /// <returns></returns>
        public List<Post> GetPostsByUsername(string username)
        {
            IMongoCollection<Post> collection = _database.GetCollection<Post>("posts");
            return collection.Find(x => x.Username == username).ToList();
        }

        public void InsertPosts(List<ITweet> tweets)
        {
            List<Post> posts = new List<Post>();

            foreach(var tweet in tweets)
            {
                Post post = new Post(tweet);
                posts.Add(post);
            }
            
            IMongoCollection<Post> collection = _database.GetCollection<Post>("posts");
            collection.InsertMany(posts);
        }
    }
}
