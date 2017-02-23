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
using SMETA.DataAccess.Models;

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
        private static List<Post> _tweetBuffer;
        private static int BUFFER_MAX = 100;
        private static int SAVE_THRESHOLD = 1;
        private static int SAVE_INTERVAL = 100;
        private static int _currentMarker;

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
            _tweetBuffer = new List<Post>();
            _currentMarker = 1;
        }

        private static void TweetReceived(object sender, Tweetinvi.Events.TweetReceivedEventArgs e)
        {
            if(_currentMarker <= SAVE_THRESHOLD)
            {
                TweetHub hub = new TweetHub();
                hub.PostTweet(e.Tweet.CreatedBy.Name, e.Tweet.CreatedBy.ScreenName, e.Tweet.Text);
                _tweetBuffer.Add(AnalyzeTweet(e.Tweet));

                if (_tweetBuffer.Count > BUFFER_MAX)
                {
                    _stream.StopStream();
                    _postRepository.InsertPosts(_tweetBuffer);
                    _streamThread.Start();
                }
            }

            _currentMarker++;

            if(_currentMarker > SAVE_INTERVAL)
            {
                _currentMarker = 1;
            }
        }

        private static Post AnalyzeTweet(ITweet tweet)
        {
            Post post = new Post(tweet);

            post.AddSentimentValues(SentimentAnalysisService.GetSentiment(post.Text));

            return post;
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