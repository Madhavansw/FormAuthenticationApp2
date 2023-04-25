using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DropDownApp.Models
{
    public class sampleEmp
    {

        [Key]
        public int EmpId             { get; set; }
        public string Name           { get; set; }
        public string Description    { get; set; }
        public byte[] Image          { get; set; }
        public bool IsTrained        { get; set; }
        public bool IsActive         { get; set; }
        public string Roles          { get; set; }
        public string Password       { get; set; }
        public DateTime LogOn        { get; set; }

        
    }
}