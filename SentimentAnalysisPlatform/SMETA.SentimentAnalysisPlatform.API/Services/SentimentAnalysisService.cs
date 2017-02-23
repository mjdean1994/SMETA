using SMETA.SentimentAnalysisPlatform.API.Models;
using SMETA.SentimentAnalysisPlatform.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMETA.SentimentAnalysisPlatform.API.Services
{
    public static class SentimentAnalysisService
    {
        private static List<WordSentiment> _wordSentiments { get; set; }
        
        private static void Initialize()
        {
            SentimentDictionaryRepository sentimentDictionaryRepository = new SentimentDictionaryRepository();
            _wordSentiments = sentimentDictionaryRepository.SelectAll();
        }

        public static TweetSentiment Analyze(string tweetBody)
        {
            if(_wordSentiments == null)
            {
                Initialize();
            }

            TweetSentiment tweetSentiment = new TweetSentiment();
            int validCount = 0;

            foreach(var word in tweetBody.Split(' '))
            {
                WordSentiment wordSentiment = _wordSentiments.FirstOrDefault(x => x.Word == word);

                if(wordSentiment != null)
                {
                    tweetSentiment.Positivity += wordSentiment.Positive.HasValue ? wordSentiment.Positive.GetValueOrDefault() : 0;
                    tweetSentiment.Negativity += wordSentiment.Negative.HasValue ? wordSentiment.Negative.GetValueOrDefault() : 0;
                    tweetSentiment.Anger += wordSentiment.Anger.HasValue ? wordSentiment.Anger.GetValueOrDefault() : 0;
                    tweetSentiment.Anticipation += wordSentiment.Anticipation.HasValue ? wordSentiment.Anticipation.GetValueOrDefault() : 0;
                    tweetSentiment.Disgust += wordSentiment.Disgust.HasValue ? wordSentiment.Disgust.GetValueOrDefault() : 0;
                    tweetSentiment.Fear += wordSentiment.Fear.HasValue ? wordSentiment.Fear.GetValueOrDefault() : 0;
                    tweetSentiment.Joy += wordSentiment.Joy.HasValue ? wordSentiment.Joy.GetValueOrDefault() : 0;
                    tweetSentiment.Sadness += wordSentiment.Sadness.HasValue ? wordSentiment.Sadness.GetValueOrDefault() : 0;
                    tweetSentiment.Surprise += wordSentiment.Surprise.HasValue ? wordSentiment.Surprise.GetValueOrDefault() : 0;
                    tweetSentiment.Trust += wordSentiment.Trust.HasValue ? wordSentiment.Trust.GetValueOrDefault() : 0;

                    validCount++;
                }
            }
            
            if(validCount > 0)
            {
                tweetSentiment.Positivity /= validCount;
                tweetSentiment.Negativity /= validCount;
                tweetSentiment.Anger /= validCount;
                tweetSentiment.Anticipation /= validCount;
                tweetSentiment.Disgust /= validCount;
                tweetSentiment.Fear /= validCount;
                tweetSentiment.Joy /= validCount;
                tweetSentiment.Sadness /= validCount;
                tweetSentiment.Surprise /= validCount;
                tweetSentiment.Trust /= validCount;
            }
            
            return tweetSentiment;
        }
    }
}