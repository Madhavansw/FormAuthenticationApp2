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
    public class SampleDbContext:DbContext
    {
      
      public DbSet<Country> Countries { get; set; }
      public DbSet<State> States { get; set; }
      public DbSet<City> Cities { get; set; }
      public DbSet<sampleEmp> sampleEmps { get; set; }
      public DbSet<Person> persons { get; set; }



        public SampleDbContext() : base("SampleConfig")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            //// configures one-to-many relationship
            //modelBuilder.Entity<Student>()
            //    .HasRequired<Grade>(s => s.CurrentGrade)
            //    .WithMany(g => g.Students)
            //    .HasForeignKey<int>(s => s.CurrentGradeId);




                             modelBuilder.Entity<State>()
                            .HasRequired<Country>(s => s.country)
                            .WithMany(c => c.States)
                            .HasForeignKey<int>(s => s.CountryId);

                             modelBuilder.Entity<City>()
                            .HasRequired<State>(c => c.state)
                            .WithMany(s => s.Cities)
                            .HasForeignKey(c => c.StateId);
                
                  


           
        }

    }
}