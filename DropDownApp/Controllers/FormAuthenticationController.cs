using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DropDownApp.DataModels;
using DropDownApp.Models;

namespace DropDownApp.Controllers
{
    public class FormAuthenticationController : Controller
    {


       private RoleDb1Entities db = null;
        private SampleDbContext _db = null;
      

       public FormAuthenticationController()
        {
            db = new RoleDb1Entities();
            _db = new SampleDbContext();

        }

        // GET: FormAuthentication
        [Authorize(Roles ="CRA,Administrator")]
        public ActionResult Index()
        {
            var EmpList = _db.sampleEmps.ToList();
            return View(EmpList);
        }


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel user)
        {

            if (ModelState.IsValid)
            {
                bool IsValidUser = _db
                               .sampleEmps
                               .Any(x => x.Name == user.UserName && x.Password == user.Password);
                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Index", "FormAuthentication");
                }
            }
            ModelState.AddModelError("", "Invalid Username or Password");
            return View();
            //if (ModelState.IsValid)
            //{
            //    bool IsValidUser = db.Users
            //   .Any(u => u.Username.ToLower() == user
            //   .UserName.ToLower() && user
            //   .Password == user.Password);

            //    if (IsValidUser)
            //    {
            //        FormsAuthentication.SetAuthCookie(user.UserName, false);
            //        return RedirectToAction("Index", "FormAuthentication");
            //    }
            //}
            //ModelState.AddModelError("", "invalid Username or Password");
            //return View();
        }
    }
}