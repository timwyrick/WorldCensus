using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldCensus.Models;

namespace WorldCensus.Controllers
{
    public class HomeController : Controller
    {
        private WorldCensusContext db = new WorldCensusContext();

        public ActionResult Index()
        {
            var popQuery = from country in db.Countries
                           select country;

            /* foreach(var pop in popQuery)
            {
                Debug.WriteLine(pop.Population);
            } */

            return View(popQuery);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult GetMap(FormCollection collection)
        {

            return View();
        }
    }
}