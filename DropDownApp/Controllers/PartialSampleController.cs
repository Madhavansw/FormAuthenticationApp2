using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownApp.Models;
using DropDownApp.Data;

namespace DropDownApp.Controllers
{
    public class PartialSampleController : Controller
    {

        private SampleDbContext db = null;

        public PartialSampleController()
        {
            db = new SampleDbContext();

        }
        // GET: PartialSample
        public ActionResult Index()
        {

            var sample = db.sampleEmps.ToList();
            if(sample != null)
            {
                return PartialView("_ViewSample",sample);
            }

            return View();
           

           
        }
        [HttpGet]
        [ActionName("PartialCreate")]
       public ActionResult PartialCreate()
        {

            return PartialView("_ViewCreate");
        }

        [HttpPost]
        [ActionName("PartialCreate")]
        public ActionResult PartialCreate(sampleEmp emp)
        {
           
            if(emp != null)
            {
                db.sampleEmps.Add(emp);
                db.SaveChanges();
                var list = db.sampleEmps.ToList();

                return View("Index",list); ;
            }

            return View();
            


        }



    }
}