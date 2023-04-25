using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownApp.Models;

namespace DropDownApp.Controllers
{
    public class CnstcController : Controller
    {
        private SampleDbContext db = null;


        public CnstcController()
        {
            db = new SampleDbContext();
        }
        [HttpGet]
        [ActionName("Index")]
        // GET: Cnstc
        public ActionResult Index()
        {

            var countryList = db
                .Countries
                .Select(c => new SelectListItem()
                        {
                            Text = c.Name,
                            Value = c.CountryId.ToString()
                        })
                .ToList();
            ViewBag.contry = countryList;

           //ViewBag.stateId = new List<SelectListItem>();
           // ViewBag.cityId = new List<SelectListItem>();



            return View();
        }

    [HttpGet]
    [ActionName("GetCityDataById")]
    public ActionResult GetCityDataById(int stateId)
        {



            //(from c in db.Countries
            // where c.CountryId == countryId
            // join
            // st in db.States
            // on c.CountryId equals st.CountryId
            // select new SelectListItem
            // {
            //     Text = st.Name,
            //     Value = st.StateId.ToString()

            // }).ToList();




            var cityList = (from st in db.States
                            where st.StateId == stateId
                            join
                             ct in db.Cities
                            on  st.StateId equals ct.StateId
                            select new SelectListItem
                            {
                                Text = ct.Name,
                                Value = ct.CityId.ToString()
                            }).ToList();




            return Json(cityList, "Application/json", JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        [ActionName("GetStateDataById")]
        public ActionResult GetStateDataById(int countryId)
        {


            var stateList = (from c in db.Countries
                             where c.CountryId == countryId
                             join
                             st in db.States
                             on c.CountryId equals st.CountryId
                             select new SelectListItem
                             {
                                 Text = st.Name,
                                 Value = st.StateId.ToString()

                             }).ToList();


            return Json(stateList, "Application/json",JsonRequestBehavior.AllowGet);




         
        }


    }
}