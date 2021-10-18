using System.Data.Entity;

namespace LB3.Models
{
    public class StudentContext : DbContext
    {
        /*public StudentContext()
            : base("DBContext")
        { }*/
        public DbSet<Student> Students { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<StudentContext>(null);
            base.OnModelCreating(modelBuilder);
        }*/
    }
}