﻿using MongoDB.Driver;
using System.Collections.Generic;
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
            _client = new MongoClient(Configuration.POST_DB_CONNECTION_STRING);
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

        public void InsertPost(ITweet tweet)
        {
            Post post = new Post(tweet);
            IMongoCollection<Post> collection = _database.GetCollection<Post>("posts");
            collection.InsertOne(post);
        }
    }
}