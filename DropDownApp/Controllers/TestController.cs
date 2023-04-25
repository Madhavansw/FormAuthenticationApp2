using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownApp.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace DropDownApp.Controllers
{
    public class TestController : Controller
    {

        private SampleDbContext db = null;

        public TestController()
        {
            db = new SampleDbContext();
        }

        [HttpGet]
        [ActionName("Index")]
        // GET: Test
        public ActionResult Index()
        {

             var list =     db.sampleEmps.ToList();

            List<Employee> empList = new List<Employee>();
            //byte[] Img = null;
            //string ImgPath = null;
            //string 

            foreach(var item in list)
            {
                //string.Format("data:image/png;base64,{0}", s);

                var Img = (byte[])item.Image;
               var  ImgPath = Convert.ToBase64String(Img);
                var link = string.Format("Data:image/png;base64,{0}", ImgPath);

                Employee emp = new Employee()
                {
                    Name = item.Name,
                    Description = item.Description,
                    Imagepath = link

                };

                empList.Add(emp);


            }
            

           
            return View(empList.ToPagedList(1, 1));


        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create(Employee emp)
        {
            byte[] data = null;

            using(Stream fs = emp.Imagefile.InputStream)
            {
               using(BinaryReader br = new BinaryReader( fs))
                {

                    data = (byte[])br.ReadBytes( (int)fs.Length);

                }
            }


            sampleEmp em = new sampleEmp()
            {
                Name = emp.Name,
                Description = emp.Description,
                Image = data

            };


            db.sampleEmps.Add(em);
            db.SaveChanges();

           


            return View();

        }





    }
}