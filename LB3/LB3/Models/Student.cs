using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LB3.Models
{
    public class Student
    {
        public Student() { }
        /*public Student(int id, string name, string phone)
        {
            this.ID = id;
            this.NAME = name;
            this.PHONE = phone;
        }*/
        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
        public string PHONE { get; set; }

        [NotMapped]
        public HateoasLinks _links;

        /*
        public override string ToString()
        {
            return $"ID - {this.ID}\n" + $"NAME - {this.NAME}\n" + $"PHONE - {this.PHONE}";
        }*/
    }
}