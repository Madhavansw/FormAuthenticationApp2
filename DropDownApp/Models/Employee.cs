using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DropDownApp.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string  Imagepath { get; set; }

        public  HttpPostedFileBase Imagefile { get; set; }

    }
}