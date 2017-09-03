using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCensus.ViewModels
{
    public class SingleDataViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public double? Data { get; set; }
        public string DataName { get; set; }
    }
}