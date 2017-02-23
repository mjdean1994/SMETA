using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMETA.DataAccess.Models
{
    public class SentimentScore
    {
        public float positivity { get; set; }
        public float negativitiy { get; set; }
        public float anger { get; set; }
        public float anticipation { get; set; }
        public float disgust { get; set; }
        public float fear { get; set; }
        public float joy { get; set; }
        public float sadness { get; set; }
        public float surprise { get; set; }
        public float trust { get; set; }
    }
}
