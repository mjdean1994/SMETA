using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMETA.DataAccess.Models
{
    public class PostFilter
    {
        public PostFilter()
        {
            StartDate = new DateTime(1, 1, 1);
            EndDate = new DateTime(9999, 12, 31);
            Query = "";
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Query { get; set; }
    }
}
