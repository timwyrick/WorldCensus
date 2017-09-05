using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCensus.Models
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Continent Continent { get; set; }
        public int Population { get; set; }
        public double? LifeExpectancy { get; set; }
        public double? TotalArea { get; set; }
        public double? GDP { get; set; }
        public string Synopsis { get; set; }
        public string FilePath1 { get; set; }
        public string FilePath2 { get; set; }
        public string FilePath3 { get; set; }
        public int FoundedDate { get; set; }
        public int EndDate { get; set; }
    }
}