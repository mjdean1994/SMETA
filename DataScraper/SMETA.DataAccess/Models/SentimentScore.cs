using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMETA.DataAccess.Models
{
    public class SentimentScore
    {
        public string label { get; set; }
        public class Probability
        {
            public float neg { get; set; }
            public float neutral { get; set; }
            public float pos { get; set; }
        }

        public Probability probability { get; set; }
    }
}
