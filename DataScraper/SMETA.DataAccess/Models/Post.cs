using System;
using Tweetinvi.Models;

namespace SMETA.DataAccess.Models
{
    public class Post
    {
        public Post(ITweet tweet)
        {
            DisplayName = tweet.CreatedBy.Name;
            Username = tweet.CreatedBy.ScreenName;
            Text = tweet.Text;
            PostedDate = DateTime.Now;
            Updated = DateTime.Now;
        }

        public void AddSentimentValues(SentimentScore sentimentScore)
        {
            Sentiment = ((sentimentScore.positivity - sentimentScore.negativitiy) + 1) / 2;
            Anger = sentimentScore.anger;
            Anticipation = sentimentScore.anticipation;
            Disgust = sentimentScore.disgust;
            Fear = sentimentScore.fear;
            Joy = sentimentScore.joy;
            Sadness = sentimentScore.sadness;
            Surprise = sentimentScore.surprise;
            Trust = sentimentScore.trust;
            Updated = DateTime.Now;
        }

        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime PostedDate { get; set; }
        public float Sentiment { get; set; }
        public float Anger { get; set; }
        public float Anticipation { get; set; }
        public float Disgust { get; set; }
        public float Fear { get; set; }
        public float Joy { get; set; }
        public float Sadness { get; set; }
        public float Surprise { get; set; }
        public float Trust { get; set; }
        public DateTime Updated { get; set; }
    }
}
