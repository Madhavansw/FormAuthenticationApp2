using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownApp.Models;
using PagedList.Mvc;
using PagedList;

namespace DropDownApp.Controllers
{
    public class PagingController : Controller
    {

        private SampleDbContext db = null;

        public PagingController()
        {

            db = new SampleDbContext();
        }
        // GET: Paging
        public ActionResult Index(string Sorting_Order, string Search_Data,string Filter_Value, int? Page_No)
        {

            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Decending" : "";
            ViewBag.SortingDate = Sorting_Order == "Date_Acending" ? "Date_Dcending" : "Date_Acending";

            if(Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }


            ViewBag.FilterValue = Search_Data;


            var MultpleEmp = db
                           .sampleEmps
                           .Select(x => x)
                           .ToList();

            if(Search_Data != null)
            {
                MultpleEmp = MultpleEmp
                    .Where(x => x.Name.Contains(Search_Data))
                    .ToList();

            }
          

            switch (Sorting_Order)
            {
                case "Name_Decending":
                    MultpleEmp = MultpleEmp
                                .OrderByDescending(x => x.Name)
                               .ToList();
                    break;
                case "Date_Acending":
                    MultpleEmp = MultpleEmp
                               .OrderBy(x => x.LogOn)
                               .ToList();
                    break;
                case "Date_Dcending":
                    MultpleEmp = MultpleEmp
                                .OrderByDescending(x => x.LogOn)
                                .ToList();
                    break;

              
                default:
                    MultpleEmp = MultpleEmp
                           .OrderBy(x => x.Name)
                           .ToList();
                    break;
            }
            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);
            return View(MultpleEmp.ToPagedList(No_Of_Page, Size_Of_Page));




        }
    }
}