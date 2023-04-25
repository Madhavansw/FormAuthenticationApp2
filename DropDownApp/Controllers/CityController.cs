using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownApp.Models;


namespace DropDownApp.Controllers
{
    public class CityController : Controller
    {
        private SampleDbContext db = null;




        public CityController()
        {
            db = new SampleDbContext();
        }
        // GET: City
        public ActionResult Index()
        {

            var cityList = db.Cities.ToList();
            
            return View(cityList);
        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create()
        {

            ViewBag.stateList = db
                               .States
                               .Select(x => new SelectListItem()
                               {
                                   Text = x.Name,
                                   Value = x.StateId.ToString()

                               })
                               .ToList();



            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create(City city)
        {

            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();

            



        }





    }
}