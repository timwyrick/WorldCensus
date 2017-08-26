using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldCensus.Models;
using WorldCensus.ViewModels;

namespace WorldCensus.Controllers
{
    public class HomeController : Controller
    {
        private WorldCensusContext db = new WorldCensusContext();

        public ActionResult Index()
        {
            var popQuery = (from country in db.Countries
                            select country);

            /* foreach(var pop in popQuery)
            {
                Debug.WriteLine(pop.Population);
            } */

            return View(popQuery);
        }

        public ActionResult Africa()
        {
            IQueryable list = GetData("Africa", "population");

            return View(list);
        }

        public ActionResult Asia()
        {
            return View();
        }

        public ActionResult Europe()
        {
            IQueryable list = GetData("Europe", "population");

            return View(list);
        }

        public ActionResult Oceania()
        {
            return View();
        }

        public ActionResult NorthAmerica()
        {
            return View();
        }

        public ActionResult SouthAmerica()
        {
            return View();
        }

        public ActionResult World()
        {
            return View();
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


        /**
         * This function takes in form data from the above navbar to determine which view should be
         * selected to be shown. If the form is submitted without entering data, the function will 
         * return the worldmap with population data.
         * 
         * INPUTS:
         *  collection: a FormCollection containing the form data selections from the navbar on the page.
         **/
        [HttpPost]
        public ActionResult GetMap(FormCollection collection)
        {
            //If the form has not been submitted, default to returning index.html
            if (!collection.AllKeys.Contains("map"))
            {
                var data = GetData("world", "population");
                return View("Index");
            }
            
            else
            {
                //If the form does not have a data type submitted, default to population
                if(!collection.AllKeys.Contains("dataype"))
                {
                    var data = GetData(collection["map"], "population");
                    return View(collection["map"], data);
                }
                else
                {
                    var data = GetData(collection["map"], collection["datatype"]);
                    return View(collection["map"], data);
                }
            }
        }


        /**
         * Takes the datatype passed in from the webpage's navbar form and returns the data associated
         * with the datatype.
         * 
         * INPUTS:
         *  map: the map to be displayed.
         *  datatype: the type of data (population,age,area,etc.) wanted to display on the page.
         * 
         **/
        public IQueryable GetData(string map, string datatype)
        {


            IQueryable<int> getMap;

            if(map == "world")
            {
                    getMap = from continents in db.Continents
                             select continents.ID;
            }
            else
            {
                    getMap = from continents in db.Continents
                             where continents.Name == map
                             select continents.ID;
            }

            //List<string,string,int> getData;

            /*var getData = Enumerable.Empty<object>()
             .Select(b => new { Name = "", Code= "", Population =  0}) // prototype of anonymous type
             .ToList(); */

            IQueryable getData;
            switch (datatype)
            {
                case "population":
                       getData = (from country in db.Countries
                                  where getMap.Contains(country.Continent.ID) //country.Continent.ID == getMap.First()
                                  select new PopulationViewModel()
                                  {
                                      Name = country.Name,
                                      Code = country.Code,
                                      Population = country.Population
                                  });        
                    break;

                case "area":

                    getData = (from country in db.Countries
                              where getMap.Contains(country.Continent.ID)
                              select new Country()
                              {
                                  Name = country.Name,
                                  Code = country.Code,
                                  Population = country.Population
                              });
                    break;

                case "medianage":

                    getData = (from country in db.Countries
                              where getMap.Contains(country.Continent.ID)
                              select new Country()
                              {
                                  Name = country.Name,
                                  Code = country.Code,
                                  Population = country.Population
                              });
                    break;

                default:

                    getData = (from country in db.Countries
                              where getMap.Contains(country.Continent.ID)
                              select new Country()
                              {
                                  Name = country.Name,
                                  Code = country.Code,
                                  Population = country.Population
                              });
                    break;
            }

             return getData;
        }
    }
}