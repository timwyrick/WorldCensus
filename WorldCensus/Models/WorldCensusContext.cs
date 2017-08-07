using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;


namespace WorldCensus.Models
{
    public class WorldCensusContext : DbContext
    {
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}