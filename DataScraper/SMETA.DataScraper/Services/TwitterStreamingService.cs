using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Configuration;
using SMETA.DataScraper.Hubs;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Streaming;
using SMETA.DataAccess.Repositories;

namespace SMETA.DataScraper.Services
{
    public static class TwitterStreamingService
    {
        /// <summary>
        /// 0 - Offline, 1 - Online
        /// </summary>
        public static bool Status;

        private static Thread _streamThread;
        private static ISampleStream _stream;
        private static string _consumerKey;
        private static string _consumerSecret;
        private static string _accessToken;
        private static string _accessSecret;
        private static PostRepository _postRepository;
        private static List<ITweet> _tweetBuffer;
        private static int BUFFER_MAX = 10000;

        private static void _initializeStream()
        {
            _consumerKey = WebConfigurationManager.AppSettings["Twitter_ConsumerKey"];
            _consumerSecret = WebConfigurationManager.AppSettings["Twitter_ConsumerSecret"];
            _accessToken = WebConfigurationManager.AppSettings["Twitter_AccessToken"];
            _accessSecret = WebConfigurationManager.AppSettings["Twitter_AccessSecret"];

            Auth.SetUserCredentials(_consumerKey, _consumerSecret, _accessToken, _accessSecret);

            _postRepository = new PostRepository();

            _stream = Stream.CreateSampleStream(Auth.Credentials);
            _stream.AddTweetLanguageFilter(Language.English);
            _stream.TweetReceived += TweetReceived;
            _tweetBuffer = new List<ITweet>();
        }

        private static void TweetReceived(object sender, Tweetinvi.Events.TweetReceivedEventArgs e)
        {
            TweetHub hub = new TweetHub();
            hub.PostTweet(e.Tweet.CreatedBy.Name, e.Tweet.CreatedBy.ScreenName, e.Tweet.Text);
            _tweetBuffer.Add(e.Tweet);

            if(_tweetBuffer.Count > BUFFER_MAX)
            {
                _postRepository.InsertPosts(_tweetBuffer);
            }
        }

        public static void StartStream()
        {
            if(_stream == null)
            {
                _initializeStream();
            }

            _streamThread = new Thread(new ThreadStart(_stream.StartStream));
            _streamThread.Start();
            Status = true;
        }

        public static void StopStream()
        {
            _stream.StopStream();
            _streamThread.Abort();
            Status = false;

            //clear the buffer
            _postRepository.InsertPosts(_tweetBuffer);
        }
    }
}