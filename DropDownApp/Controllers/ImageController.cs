using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownApp.Models;
using DropDownApp.Data;
using System.Drawing;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace DropDownApp.Controllers
{
    public class ImageController : Controller
    {

        private SampleDbContext db = null;
        //private ImageDbEntities db = null; 

        public ImageController()
        {
            //db = new ImageDbEntities();
            db = new SampleDbContext();

        }


        [HttpGet]
        [ActionName("Index")]
        // GET: Image
        public ActionResult Index()
         {
            return View();
        }
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Index(Employee emp)
        {
            if (ModelState.IsValid)
            {

                using (Stream fs = emp.Imagefile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        sampleEmp _emp = new sampleEmp()
                        {
                           
                            Name = emp.Name,
                            Description = emp.Description,
                            Image = bytes
                        };

                        db.sampleEmps.Add(_emp);
                        db.SaveChanges();



                        return RedirectToAction("ShowImageData");





                    }
                }

            }

            return View();
        }

        [HttpGet]
        [ActionName("ShowImageData")]
        public ActionResult ShowImageData()
         {

            //MemoryStream ms = new MemoryStream(img);
            //Image1 = Image.FromStream(ms);

            //byte[] img = (byte[])dt.Rows[0]["logo"];
            //string s = System.Text.Encoding.ASCII.GetString(img);
            //Image1.ImageUrl = "~/" + s;

            //Image image = Image.FromFile(@"C:\Images\3D art Wallpapers\Glass_Sphere.jpg");
            //MemoryStream memStream = new MemoryStream();
            //image.Save(memStream,image.RawFormat);
            //byte[] array = memStream.ToArray();
            //string imageString = Convert.ToBase64String(array);



            //Image image = Image.FromFile(@"C:\Images\3D art Wallpapers\Glass_Sphere.jpg");
            //MemoryStream memStream = new MemoryStream();
            //image.Save(memStream, image.RawFormat);
            //byte[] array = memStream.ToArray();
            //string imageString = Convert.ToBase64String(array);



            //#region Creating image from string
            //string imageString = "read the image string from XML file";
            //byte[] imageBytes = Convert.FromBase64String(imageString);
            //MemoryStream memStream = new MemoryStream(imageBytes);
            //Image image = Image.FromStream(memStream);



            //#region Creating image from string
            //string imageString = "read the image string from XML file";
            //byte[] imageBytes = Convert.FromBase64String(imageString);
            //MemoryStream memStream = new MemoryStream(imageBytes);
            //Image image = Image.FromStream(memStream);








            //byte[] img = null;

            //var existing = db
            //                //.Emps
            //                .Where(x => x.EmpId == Id)
            //                .SingleOrDefault();

            //if(existing != null)


            //{

            //    img =  (byte[])existing.Image;
            //    //string s = System.Text.Encoding.ASCII.GetString(img);
            //    string s = Convert.ToBase64String(img);
            //    //MemoryStream ms = new MemoryStream(img);
            //    //Image1 = Image.FromStream(ms);
            //    //MemoryStream ms = new MemoryStream(img);


            var sample = db.sampleEmps.ToList();

            byte[] imag = null;
            string s = null;
            string ImageLink = null;
            List<Employee> listemp = new List<Employee>();


            foreach(var item in sample)
            {
                imag = (byte[])item.Image;
                s = Convert.ToBase64String(imag);
                ImageLink = string.Format("data:image/png;base64,{0}", s);
                Employee emp = new Employee()
                {
                    Name = item.Name,
                    Description = item.Description,
                    Imagepath = ImageLink
                };
                listemp.Add(emp);


                
            }

            //value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});


            //var value = JsonConvert.SerializeObject(listemp,
            //    Formatting.Indented,
            //    new JsonSerializerSettings
            //    {
            //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //    });


            //JavaScriptSerializer jsJson = new JavaScriptSerializer();
            // jsJson.MaxJsonLength = 2147483644;
            // listemp  = jsJson.Deserialize<Employee>(json_object);




            return Json(listemp, "application/json", JsonRequestBehavior.AllowGet);



            //    Employee emp = new Employee()
            //    {
            //        EmpId = existing.EmpId,
            //        Name = existing.Name,
            //        Description = existing.Description,
            //        Imagepath = string.Format("data:image/png;base64,{0}",s)
                  
                     


                   

            //    };

            //    //ViewBag.Data = string.Format("data:image/png;base64,{0}", s);

               

            //    return View(emp);



            //}


         
                           





         
        }




    }
}