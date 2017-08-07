using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCensus.Models
{
    public class Continent
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
    }
}