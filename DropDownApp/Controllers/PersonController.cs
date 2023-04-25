using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownApp.Models;
using DropDownApp.ModelView;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Text;
using System.Web.Security;
using System.Globalization;

namespace DropDownApp.Controllers
{


    public class PersonController : Controller
    {

        private SampleDbContext db = null;


        public PersonController()
        {
            db = new SampleDbContext();

        }


        [HttpGet]
        [ActionName("Index")]
        [Authorize]
        // GET: Person
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {






            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            List<PersonModelView> plist = new List<PersonModelView>();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;


            var persons = (from p in db.sampleEmps
                           select new PersonModelView
                           {
                               Id = p.EmpId,
                               Name = p.Name,
                               Description = p.Description,
                               ImageByte = p.Image,
                               LogOn = p.LogOn

                           }).ToList();


            foreach (var item in persons)
            {

                string Image = null;

                if (item.ImageByte != null)
                {

                    Image = Convert.ToBase64String(item.ImageByte);

                }



                var pv = new PersonModelView();


                pv.Id = item.Id;
                pv.Name = item.Name;
                pv.Description = item.Description;
                pv.ImagePath = string.Format("data:image/png;base64,{0}", Image);
                //var datediff =  Convert.ToInt16(item.LogOn - DateTime.Today);
                //TimeSpan ts1 = new TimeSpan(pv.LogOn.Hour,pv.LogOn.Minute,pv.LogOn.Second);
                //TimeSpan ts2 = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                //if ((ts1 - ts2) == 0)
                //var result = item.LogOn.CompareTo(DateTime.Now);
                //if( result == 0)
                pv.LogOnDateTime = item.LogOn.ToString("HH:mm");
                var result = pv.LogOnDateTime.IndexOf("00:00");
                if (result == -1)
                    pv.LogOnDateTime = item.LogOn.ToString("dd/MM/yyyy HH:mm:ss");
                else
                    pv.LogOnDateTime = "";

                //ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                //else
                //pv.LogOnDateTime = "Null";




                plist.Add(pv);

            }

            if (!String.IsNullOrEmpty(searchString))
            {
                plist = plist.Where(p => p.Name.Contains(searchString)).ToList();

            }



            switch (sortOrder)
            {
                case "name_desc":
                    plist = plist.OrderByDescending(p => p.Name).ToList();
                    break;

                default:  // Name ascending 
                    plist = plist.OrderBy(p => p.Name).ToList();
                    break;
            }

            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(plist.ToPagedList(pageNumber, pageSize));


        }

        [HttpGet]
        [ActionName("LogOn")]
        public ActionResult LogOn()
        {

            return View();
        }

        [HttpPost]
        [ActionName("LogOn")]
        public ActionResult LogOn(EmpLoginModelView emp)
        {




            bool isActive   = IsActiveValid (emp.Name, emp.Password);
            
         

            bool isTrained = IsTrainedValid(emp.Name, emp.Password);
          
             if(isActive == false && isTrained == false)
            {
                ModelState.AddModelError("", ",This is User Not Active and Trained so There is no permision to login");
                return View();

            }


            if (isActive == false)
            {
                ModelState.AddModelError("", "This User is not Active  so There is No permission to Login");
                return View();
            }
            if (isTrained == false)
            {
                ModelState.AddModelError("", "This User is not Trained so There No permission to Login");
                return View();
            }






            //if ()
            //{



            //    if(User.IsActive != true)
            //    {
            //        ModelState.AddModelError("", "This person in not Activie!");
            //    }
            //    //else
            //    //{

            //    //}
            //    //if(User.IsTrained != true)
            //    //{
            //    //    ModelState.AddModelError("", "This User is not Trained!");
            //    //}

            var existinguser = db
                    .sampleEmps
                    .Where(x => x.Name == emp.Name && x.Password == emp.Password)
                    .SingleOrDefault();
                existinguser.LogOn = DateTime.Now;
                //existinguser.LogOn = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                db.Entry(existinguser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                FormsAuthentication.SetAuthCookie(emp.Name, false);
                return RedirectToAction("Index");
            //}
            //else
            //{
            //    ModelState.AddModelError("", " User is Not IsActive or  IsTrained");
            //    return View();
            //}


           
           

        }

        private bool IsActiveValid(string name, string password)
        {

            var user = db
                .sampleEmps
                .Where(x => x.Name == name && x.Password == password)
                .SingleOrDefault();
            bool IsActive = false;
            if (user != null)
            {

                IsActive = user.IsActive ? true : false;

            }



            return IsActive;

           
        }

        public bool IsTrainedValid(string name,string password)
        {

            var user = db
                      .sampleEmps
                      .Where(x => x.Name == name && x.Password == password)
                      .SingleOrDefault();
            bool IsTrained = false;
              if (user != null)
            {
              IsTrained  = user.IsTrained ? true : false;

            }



            return IsTrained;

            //if (user.IsTrained == true)
            //    return  true;
            //else
            //    return false;




            //bool IsUserTrained = db
            //                  .sampleEmps
            //                  .Any(x => x.IsActive == User.IsActive || x.IsTrained == User.IsTrained);

            //return IsUserTrained;

        }


        public ActionResult LogOut()
        {




            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn");



        }


        [HttpGet]
        [ActionName("OnRegistration")]
        public ActionResult OnRegistration()
        {
            return View();

        }
        [HttpPost]
        [ActionName("OnRegistration")]
        public ActionResult OnRegistration(EmpLoginModelView emp)
        {
            bool IsValidUser = db
                              .sampleEmps
                              .Any(x => x.Name == emp.Name);

            if (IsValidUser)
            {
                var existingUser = db
                              .sampleEmps
                              .Where(x => x.Name == emp.Name)
                              .SingleOrDefault();

                if (existingUser.Password == null)
                {
                    existingUser.Password = emp.Password;

                    db.Entry(existingUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("LogOn");


                }
                else
                {

                    if (existingUser.Name == emp.Name && existingUser.Password == emp.Password)
                    {
                        return RedirectToAction("LogOn");

                    }
                }

            }

            else
            {
                var NewUser = new sampleEmp()
                {
                    Name = emp.Name,
                    Password = emp.Password,
                    LogOn = DateTime.Now

                };
                db.sampleEmps.Add(NewUser);
                db.SaveChanges();
                return RedirectToAction("LogOn");

            }


            return View();
        }



        [HttpPost]
        [ActionName("DeletePerson")]
        public ActionResult DeletePerson(int Id)
        {

            if (Id < 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var deletePerson = db
                             .sampleEmps
                             .Where(x => x.EmpId == Id)
                             .SingleOrDefault();
            db.Entry(deletePerson).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();






            return Json(new { result = "successfully " });

        }

        [HttpGet]
        [ActionName("EditPerson")]
        public ActionResult EditPerson(int Id)
        {


            return View();

        }

        [HttpPost]
        [ActionName("EditPerson")]
        public ActionResult EditPerson(PersonModelView PersonModel)
        {


            if (ModelState.IsValid)
            {

                var existing = db
                               .sampleEmps
                               .Where(x => x.EmpId == PersonModel.Id)
                               .SingleOrDefault();

                existing.Name = PersonModel.Name;
                existing.Description = PersonModel.Description;
                existing.IsActive = PersonModel.IsActive;
                existing.IsTrained = PersonModel.IsTrained;
                existing.Roles = string.Join(",", PersonModel.Roles);

                byte[] data = null;

                if (PersonModel.FilePosted != null)
                {


                    using (Stream st = PersonModel.FilePosted.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(st))
                        {
                            data = br.ReadBytes((Int32)st.Length);

                        }

                        existing.Image = data;

                    }
                }
                else
                {

                    existing.Image = existing.Image;
                }





                db.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();





            }

            return Json(new { result = "Successfully Edited" }, JsonRequestBehavior.AllowGet);








        }


        public ActionResult GetPersonById(int Id)
        {

            var person = db
                         .sampleEmps
                         .Where(x => x.EmpId == Id)
                         .FirstOrDefault();


            PersonModelView ob = new PersonModelView()
            {
                Id = person.EmpId,
                Name = person.Name,
                IsActive = person.IsActive,
                IsTrained = person.IsTrained,
                Description = person.Description,
                Password = person.Password,
                Roles = string.Join(",", person.Roles),


                ImagePath = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(person.Image))

            };



            return Json(ob, JsonRequestBehavior.AllowGet);


        }



        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ActionName("CreatePost")]
        //[Authorize(Roles = "Admin,Support")]
        public ActionResult CreatePost(PersonModelView personModel)
        {

            var isEmailAlreadyExists = db.sampleEmps.Any(x => x.Name == personModel.Name);

            if (isEmailAlreadyExists)
            {

                return Json(new { isValid = false, Msg = "UserName AlreadyExisting!" }, JsonRequestBehavior.AllowGet);
            }


            string fileType = Path.GetExtension(personModel.FilePosted.FileName);
            if (!CheckFileType(fileType))
            {


                return Json(new { isValid = false, Msg = "File Extentenstion doennot Alloed!" });

            }





            if (ModelState.IsValid)
            {
                byte[] Imgebytes = null;

                using (Stream st = personModel.FilePosted.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(st))
                    {
                        Imgebytes = br.ReadBytes((int)st.Length);

                    }

                    sampleEmp sm = new sampleEmp()
                    {


                        Name = personModel.Name,
                        Description = personModel.Description,
                        Image = Imgebytes,
                        Password = personModel.Password,
                        LogOn = DateTime.Today,

                        IsActive = personModel.IsActive,
                        IsTrained = personModel.IsTrained,
                        Roles = string.Join(",", personModel.Roles)




                    };


                    db.sampleEmps.Add(sm);
                    db.SaveChanges();


                }

            }


            return Json(new { isValid = true, Msg = "Succfully Saved" }, JsonRequestBehavior.AllowGet);

        }




      
  
    



    bool CheckFileType(string fileName)
    {
        string ext = Path.GetExtension(fileName);
        switch (ext.ToLower())
        {
            case ".gif":
                return true;
            case ".jpg":
                return true;
            case ".jpeg":
                return true;
            case ".png":
                return true;
            default:
                return false;
        }
    }




    }
}


