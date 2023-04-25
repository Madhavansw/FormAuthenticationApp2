using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownApp.Models;
using System.Net;

namespace DropDownApp.Controllers
{
    public class CountryController : Controller
    {

        private SampleDbContext db = null;

        public CountryController()
        {
            db = new SampleDbContext();

        }
        // GET: Country
        public ActionResult Index()
        {

            var CountryList = db
                             .Countries
                             .ToList();
            return View(CountryList);
        }


       public ActionResult Edit(Country country)
        {
            if (!ModelState.IsValid)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var existing = db
                          .Countries
                          .Where(x => x.CountryId == country.CountryId)
                          .SingleOrDefault();
           




            if(existing != null)
            {


                existing.Name = country.Name;
                existing.Description = country.Description;

                db.Countries.Attach(existing);
                db.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
               

            }


            return View();


        }




        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit(int Id)
        {

            if(Id < 0)
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest);
            }

            var existing = db.Countries
                             .Where(x => x.CountryId == Id)
                             .SingleOrDefault();
             if(existing == null)
            {
                return HttpNotFound();
            }


            return View(existing);




        }






        [HttpGet]
        [ActionName("Details")]
        public ActionResult Details(int Id)
        {
            if(Id < 0)
            {
                return new  HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var existing = db
                           .Countries
                           .Where(x => x.CountryId == Id)
                           .FirstOrDefault();

            if(existing == null)
            {
                return HttpNotFound();
            }


            return View(existing);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if(id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var existing = db
                           .Countries
                           .Where(x => x.CountryId == id)
                           .SingleOrDefault();
            if(existing != null)
            {
                db.Countries.Remove(existing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            return View();
        }



        [HttpGet]
        [ActionName("Delete")]
        public ActionResult CountyDelete( int Id)
        {

             if(Id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var countryDelete = db
                                .Countries
                                .Select(c => c)
                                .Where(c => c.CountryId == Id)
                                .SingleOrDefault();
            if(countryDelete == null)
            {
                return HttpNotFound();
            }



            return View(countryDelete);
        }



        [HttpGet]
        [ActionName("Create")]
        public ActionResult CountryCreate()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CountryCreate(Country county)
        {
            if (ModelState.IsValid)
            {

                db.Countries.Add(county);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();

            
        }
    }
}