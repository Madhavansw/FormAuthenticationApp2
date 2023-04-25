using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;


namespace DropDownApp.ModelView
{
    public class PersonModelView 
    {

        
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public byte[] ImageByte { get; set; }
        public bool IsActive { get; set; }
        public bool IsTrained { get; set; }
        public string Roles { get; set; }
        public DateTime LogOn { get; set; }
        public string LogOnDateTime { get; set; }
        public string Password { get; set; }


       public  HttpPostedFileBase  FilePosted { get; set; }
    }
}