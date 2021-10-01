using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LB3.Models
{
    public class StudentsContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<StudentsContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}