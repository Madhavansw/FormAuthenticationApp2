using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DropDownApp.Models
{
    [Table("Country")]
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string  Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}