//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using LB6_7.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.SqlClient;
using System.Data.Services.Common;
using System.Data.Services.Providers;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using System.Data.Entity;
using System.ServiceModel;

namespace LB6_7
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [DataServiceKey("id")]
    public class MainWcfDataService : EntityFrameworkDataService<WSSDAEntities>
    {
        private WSSDAEntities context = HomeController.CONTEXT;
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            // config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);

            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;

            //config.UseVerboseErrors = true;
        }

        [WebGet]
        public IQueryable<Student> getStudents()
        {
            try
            {
                var studQuery = from stud in context.Student where stud.id > 0 select stud;
                return studQuery;
            }
            catch(Exception e)
            {
                throw new ApplicationException(string.Format("An error occurred: {0}", e.Message));
            }
            
        }

        [WebGet]
        public IQueryable<Note> getNotes()
        {
            try
            {
                var noteQuery = from note in context.Note where note.id > 0 select note;
                return noteQuery;
            }
            catch (Exception e)
            {
                throw new ApplicationException(string.Format("An error occurred: {0}", e.Message));
            }

        }

        [WebInvoke(Method = "POST")]
        public void addStudent(string name)
        {
            Student newStud = new Student();
            newStud.name = name;
            context.Student.Add(newStud);
            context.SaveChanges();
        }
        
        [WebInvoke(Method = "POST")]
        public void updateStudent(string id, string newName)
        {
            Student stud = context.Student.Find(int.Parse(id));
            if (stud != null)
            {
                stud.name = newName;

                context.Entry(stud).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        [WebInvoke(Method = "POST")]
        public void deleteStudent(string id)
        {
            Student stud = context.Student.Find(int.Parse(id));
            if (stud != null)
            {
                var studNotes = from obj in context.Note where obj.studentId == stud.id select obj;

                foreach (var note in studNotes)
                {
                    context.Note.Remove(note);
                }
                
                context.Student.Remove(stud);
                context.SaveChanges();
            }
        }

        
        [WebInvoke(Method = "POST")]
        public void addNote(int studId, int mark, string subject)
        {
            try
            {
                Note newNote = new Note();

                newNote.studentId = studId;
                newNote.note1 = mark;
                newNote.subject = subject;

                context.Note.Add(newNote);
                context.SaveChanges();
            }
            catch(SqlException e)
            {
                throw new ApplicationException(string.Format("No such student with ID: {0}", e.Message));
            }
            
        }
        [WebInvoke(Method = "POST")]
        public void updateNote(string id, int newStudId, int newMark, string newSubj)
        {
            Note note = context.Note.Find(int.Parse(id));
            if (note != null)
            {
                note.studentId = newStudId;
                note.note1 = newMark;
                note.subject = newSubj;

                context.Entry(note).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        [WebInvoke(Method = "POST")]
        public void deleteNote(string id)
        {
            Note note = context.Note.Find(int.Parse(id));
            if (note != null)
            {
                context.Note.Remove(note);
                context.SaveChanges();
            }
        }
    }
}
