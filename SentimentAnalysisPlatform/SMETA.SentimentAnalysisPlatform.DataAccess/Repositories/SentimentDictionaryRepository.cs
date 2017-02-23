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

        public List<WordSentiment> SelectAll()
        {
            using (var context = new SentimentDictionaryDbEntities())
            {
                context.Database.Connection.Open();
                return context.WordSentiments.ToList();
            }
        }
    }
}
