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
    [Table("State")]
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public virtual Country country { get; set; }

        public virtual ICollection<City> Cities { get; set; }


    }
}