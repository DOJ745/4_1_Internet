using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSSDAModel;

namespace LB6_CLIENT
{
    class Program
    {
        static void Main(string[] args)
        {
            WSSDAEntities entities = new WSSDAEntities(new Uri("http://localhost:56386/MainWcfDataService.svc/"));
            foreach (var student in entities.Student)
            {
                Console.WriteLine("ID: " + student.id + " " + student.name);
                List<Note> notes = entities.Note.Where(note => note.studentId == student.id).ToList();
                foreach (var note in notes)
                {
                    Console.WriteLine("------" + note.subject + ": " + note.note1);
                }
            }
        }
    }
}
