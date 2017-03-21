using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMETA.Web.ViewModels
{
    public class DataViewModel
    {
        public string date { get; set; }
        public string sentiment { get; set; }
        public string positivity { get; set; }
        public string neutrality { get; set; }
        public string negativity { get; set; }
    }
}