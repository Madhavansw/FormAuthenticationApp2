using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownApp.Models;

namespace DropDownApp.Controllers
{
    public class AjaxCrudController : Controller
    {

        private SampleDbContext db = null;

        public AjaxCrudController()
        {
            db = new SampleDbContext();

        }
        // GET: AjaxCrud
        public ActionResult Index()
        {
            var empList = db.sampleEmps.ToList();
            return View(empList);
        }

        public ActionResult AddEmp(sampleEmp emp)
        {




            return Json(new { emp = emp }, JsonRequestBehavior.AllowGet);
        }
    }
}