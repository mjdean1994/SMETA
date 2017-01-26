using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMETA.SentimentAnalysisPlatform.DataAccess;

namespace SMETA.DictionaryParser
{
    class Program
    {
        private static char LINE_DELIMITER = '\t';

        static void Main(string[] args)
        {
            Console.WriteLine("~ SMETA EmoLex Importer ~");
            Console.Write("File Location: ");
            string filePath = Console.ReadLine();

            Console.WriteLine("Searching for EmoLex at {0}...", filePath);

            StreamReader file = new StreamReader(filePath);

            if(null == file)
            {
                Console.WriteLine("Failed to find EmoLex. Please make sure the file name and location are correct and try again.");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
                return;
            }

            while(!file.EndOfStream)
            {
                WordSentiment wordSentiment = new WordSentiment();

                string line = file.ReadLine();
                string[] lineComponents = line.Split(LINE_DELIMITER);
                wordSentiment.Word = lineComponents[0];
                wordSentiment.Anger = Convert.ToInt32(lineComponents[2]);

                line = file.ReadLine();
                lineComponents = line.Split(LINE_DELIMITER);
                wordSentiment.Anticipation = Convert.ToInt32(lineComponents[2]);

                line = file.ReadLine();
                lineComponents = line.Split(LINE_DELIMITER);
                wordSentiment.Disgust = Convert.ToInt32(lineComponents[2]);

                line = file.ReadLine();
                lineComponents = line.Split(LINE_DELIMITER);
                wordSentiment.Fear = Convert.ToInt32(lineComponents[2]);

                line = file.ReadLine();
                lineComponents = line.Split(LINE_DELIMITER);
                wordSentiment.Joy = Convert.ToInt32(lineComponents[2]);

                line = file.ReadLine();
                lineComponents = line.Split(LINE_DELIMITER);
                wordSentiment.Negative = Convert.ToInt32(lineComponents[2]);

                line = file.ReadLine();
                lineComponents = line.Split(LINE_DELIMITER);
                wordSentiment.Positive = Convert.ToInt32(lineComponents[2]);

                line = file.ReadLine();
                lineComponents = line.Split(LINE_DELIMITER);
                wordSentiment.Sadness = Convert.ToInt32(lineComponents[2]);

                line = file.ReadLine();
                lineComponents = line.Split(LINE_DELIMITER);
                wordSentiment.Surprise = Convert.ToInt32(lineComponents[2]);

                line = file.ReadLine();
                lineComponents = line.Split(LINE_DELIMITER);
                wordSentiment.Trust = Convert.ToInt32(lineComponents[2]);

                Console.WriteLine("Added Word: {0}", wordSentiment.Word);
                SentimentDictionaryRepository sentimentDictionaryRepository = new SentimentDictionaryRepository();
                sentimentDictionaryRepository.Insert(wordSentiment);
            }
        }
    }
}
