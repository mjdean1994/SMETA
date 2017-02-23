using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMETA.SentimentAnalysisPlatform.API.Models
{
    public class TweetSentiment
    {
        public float Positivity { get; set; }
        public float Negativity { get; set; }
        public float Anger { get; set; }
        public float Anticipation { get; set; }
        public float Disgust { get; set; }
        public float Fear { get; set; }
        public float Joy { get; set; }
        public float Sadness { get; set; }
        public float Surprise { get; set; }
        public float Trust { get; set; }
    }
}