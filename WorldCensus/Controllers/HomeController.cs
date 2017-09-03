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
            /*var popQuery = (from country in db.Countries
                            select country);

            */
            var query = GetData("world", "population");
            ViewBag.DataType = "Population";

            /* foreach(var pop in popQuery)
            {
                Debug.WriteLine(pop.Population);
            } */

            return View(query);
        }

        public ActionResult Africa()
        {
            return View();
        }

        public ActionResult Asia()
        {
            return View();
        }

        public ActionResult Europe()
        {
            return View();
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

        public ActionResult Modal(string dataname)
        {
            ViewBag.Country = dataname;
            return PartialView();
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
                //FIX THIS LATER (Needs to have a preselected value in form to prevent messy bugs
                ViewBag.DataType = collection["datatype"];
                IQueryable data = GetData("world", collection["datatype"]);
                return View("Index", data);
            }
            
            else
            {
                //If the form does not have a data type submitted, default to population
                if(!collection.AllKeys.Contains("datatype"))
                {
                    ViewBag.DataType = "Population";
                    IQueryable data = GetData(collection["map"], "population");
                    return View(collection["map"].Replace(" ",string.Empty), data);
                }
                else
                {
                    ViewBag.DataType = collection["datatype"];
                    IQueryable data = GetData(collection["map"], collection["datatype"]);
                    return View(collection["map"].Replace(" ", string.Empty), data);
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
                case "Population":
                       getData = (from country in db.Countries
                                  where getMap.Contains(country.Continent.ID) //country.Continent.ID == getMap.First()
                                  select new SingleDataViewModel()
                                  {
                                      Name = country.Name,
                                      Code = country.Code,
                                      Data = country.Population
                                  });        
                    break;

                case "Total Area":

                    getData = (from country in db.Countries
                              where getMap.Contains(country.Continent.ID)
                              select new SingleDataViewModel()
                              {
                                  Name = country.Name,
                                  Code = country.Code,
                                  Data = country.TotalArea
                              });
                    break;

                case "Life Expectancy":

                    getData = (from country in db.Countries
                              where getMap.Contains(country.Continent.ID)
                              select new SingleDataViewModel()
                              {
                                  Name = country.Name,
                                  Code = country.Code,
                                  Data = country.LifeExpectancy
                              });
                    break;

                case "Gross Domestic Product (GDP)":

                    getData = (from country in db.Countries
                               where getMap.Contains(country.Continent.ID)
                               select new SingleDataViewModel()
                               {
                                   Name = country.Name,
                                   Code = country.Code,
                                   Data = country.GDP
                               });
                    break;
                default:

                    getData = (from country in db.Countries
                              where getMap.Contains(country.Continent.ID)
                              select new SingleDataViewModel()
                              {
                                  Name = country.Name,
                                  Code = country.Code,
                                  Data = country.Population
                              });
                    break;
            }

             return getData;
        }
    }
}