using System;
using Tweetinvi.Models;
using MongoDB.Bson;

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
            Positivity = sentimentScore.probability.pos;
            Neutrality = sentimentScore.probability.neutral;
            Negativity = sentimentScore.probability.neg;

            if (sentimentScore.label == "pos")
            {
                Sentiment = Positivity;
            }
            else if (sentimentScore.label == "neg")
            {
                Sentiment = 1 - Negativity;
            }
            else
            {
                Sentiment = 0.5f;
            }
        }

        public BsonObjectId  _id { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime PostedDate { get; set; }
        public float Sentiment { get; set; }
        public float Positivity { get; set; }
        public float Neutrality { get; set; }
        public float Negativity { get; set; }
        public DateTime Updated { get; set; }
    }
}
