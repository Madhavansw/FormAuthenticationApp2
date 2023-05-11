using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownApp.Models;
using DropDownApp.DataModels;

namespace DropDownApp.Controllers
{
    public class FormValidController : Controller
    {
        // GET: FormValid
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ActionName("SiteCreateMng")]
        public ActionResult SiteCreateMng( SiteMng mng)
        {



            return Json(new { Msg = true }, JsonRequestBehavior.AllowGet);

        }
    }
}