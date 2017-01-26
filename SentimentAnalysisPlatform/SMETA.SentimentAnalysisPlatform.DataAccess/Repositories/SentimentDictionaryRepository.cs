using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMETA.SentimentAnalysisPlatform.DataAccess
{
    public class SentimentDictionaryRepository
    {
        public void Insert(WordSentiment wordSentiment)
        {
            using (var context = new SentimentDictionaryDbEntities())
            {
                context.WordSentiments.Add(wordSentiment);

                context.SaveChanges();
            }
        }
    }
}
