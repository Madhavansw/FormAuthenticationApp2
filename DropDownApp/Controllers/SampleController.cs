using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownApp.Models;
using System.Data;
using System.Data.Entity;
using System.Text;


namespace DropDownApp.Controllers
{
    public class SampleController : Controller
    {
        private SampleDbContext db = null;



        public SampleController()
        {
            db = new SampleDbContext();

        }


        public ActionResult ModelSample2()
        {
            return View();
        }

        public ActionResult ModelSample()
        {
            return View();
        }



        // GET: Sample
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [ActionName("LastUser")]
       public ActionResult LastUser()
        {


            //var last = (from s in db.States
            //            select s)
            //            .ToList()
            //            .Select(x => x)
            //            .LastOrDefault();
            var last = db
                        .States
                        .ToList()
                        .Take(4)
                        .OrderByDescending(x => x.StateId)
                        .ElementAtOrDefault(3);
                       
                        
                        
                        

            //var last = db
            //          .States
            //          .ToList()
            //          .Select(x => x)
            //          .Skip(5)
            //          .Select(x => x)
            //          .Take(4)
            //          .ToList();

            StringBuilder sb = new StringBuilder();
            sb.Append(last.StateId);

            //foreach (var item in last)
            //{
            //    sb.Append(item.StateId);
            //    sb.Append("  ");
            //    sb.Append(" ");
               


            //}






            //var last = db
            //        .States
            //        .Select(x => x)
            //        .LastOrDefault();

            ViewBag.ls = sb;
            return View();
        }
    }
}