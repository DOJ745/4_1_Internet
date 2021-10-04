using System;
using System.Xml.Serialization;

namespace LB3.Models
{
    [Serializable]
    public class Student
    {
        public Student() { }
        public Student(int id, string name, string phone)
        {
            this.ID = id;
            this.NAME = name;
            this.PHONE = phone;
        }
        public int ID { get; set; }
        public string NAME { get; set; }
        public string PHONE { get; set; } 
    }
}