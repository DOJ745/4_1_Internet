using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LB3.Models
{
    public class Student
    {
        public Student() { }

        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
        public string PHONE { get; set; }

        [NotMapped]
        public HateoasLinks _links;
    }
}