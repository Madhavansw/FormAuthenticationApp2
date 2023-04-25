using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownApp.Models;

namespace DropDownApp.Controllers
{
    public class StateController : Controller
    {

        private SampleDbContext db = null;

       public  StateController()
        {
            db = new SampleDbContext();
        }
        // GET: State
        public ActionResult Index()
        {
            var StateList = db.States.ToList();

            return View(StateList);
        }
        [HttpGet]
        [ActionName("Create")]
        public ActionResult CreateState()
        {
            var countyList = db
                .Countries
                .Select(x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.CountryId.ToString()

                        })
                .ToList();

            ViewBag.CountryList = countyList;

            return View();

        }


        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreateState( State state, int CountyId)
        {

            if (ModelState.IsValid)
            {

                var st = new State()
                {
                    Name = state.Name,
                    Description = state.Description,
                    CountryId = CountyId

                };





                db.States.Add(st);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            return View();
        }





    }
}