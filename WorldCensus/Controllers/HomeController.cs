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

        /**
         * Index function. Defaults to showing the JSVector world map with the population data.
         **/
        public ActionResult Index()
        {

            var query = GetData("world", "population");
            ViewBag.DataType = "Population";

            return View(query);
        }

        /**
         * Displays Africa JSVectorMap
         **/
        public ActionResult Africa()
        {
            return View();
        }

        /**
         * Displays Asia JSVectorMap
         **/
        public ActionResult Asia()
        {
            return View();
        }

        /**
         * Displays Europe JSVectorMap
         **/
        public ActionResult Europe()
        {
            return View();
        }

        /**
         * Displays Oceania JSVectorMap
         **/
        public ActionResult Oceania()
        {
            return View();
        }

        /**
         * Displays North America JSVectorMap
         **/
        public ActionResult NorthAmerica()
        {
            return View();
        }

        /**
         * Displays South America JSVectorMap
         **/
        public ActionResult SouthAmerica()
        {
            return View();
        }

        /**
         * Displays World JSVectorMap. Currently unused.
         **/
        public ActionResult World()
        {
            return View();
        }

        /**
         * The about page contains information about the site, its purpose, 
         * and proper attribution to the sources which contributed the information and images.
         **/
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /**
         * Displays a personal contact page.
         **/
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

            //Determine which map to return first
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

            //Get relevant country data according to map and data values
            IQueryable getData;
            switch (datatype)
            {
                case "Population":
                       getData = (from country in db.Countries
                                  where getMap.Contains(country.Continent.ID)
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