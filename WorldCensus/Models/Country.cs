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
        public int FoundedDate { get; set; }
        public int EndDdate { get; set; }
    }
}