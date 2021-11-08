//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace LB6_7
{
    public class MainWcfDataService : EntityFrameworkDataService<WSSDAEntities>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            // config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);

            config.SetEntitySetAccessRule("Student", EntitySetRights.All);
            config.SetEntitySetAccessRule("Note", EntitySetRights.All);

            config.SetServiceOperationAccessRule("getStudents", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("getNotes", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("getNotesByStudentId", ServiceOperationRights.All);

            config.SetServiceOperationAccessRule("addStudent", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("addNote", ServiceOperationRights.All);

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        [WebGet]
        public IQueryable<Student> getStudents(WSSDAEntities source)
        {
            try
            {
                WSSDAEntities context = source;
                var studQuery = from stud in context.Student where stud.id > 0 select stud;
                return studQuery;
            }
            catch(Exception e)
            {
                throw new ApplicationException(string.Format("An error occurred: {0}", e.Message));
            }
            
        }

        [WebGet]
        public IQueryable<Note> getNotes(WSSDAEntities source)
        {
            try
            {
                WSSDAEntities context = source;
                var noteQuery = from note in context.Note where note.id > 0 select note;
                return noteQuery;
            }
            catch (Exception e)
            {
                throw new ApplicationException(string.Format("An error occurred: {0}", e.Message));
            }

        }

        [WebGet]
        public IQueryable<Note> getNotesByStudId(int id)
        {
            if (id.Equals(null))
            {
                throw new ArgumentNullException("id",
                    "You must provide a value for the parameter 'id'.");
            }
            WSSDAEntities context = this.CurrentDataSource;
            var noteQuery = from stud in context.Note where stud.id == id select stud;
            return noteQuery;
        }

        [WebInvoke(Method = "POST")]
        public void addStudent(Student newStud, WSSDAEntities sourse)
        {
            WSSDAEntities context = sourse;
            context.Student.Add(newStud);
            context.SaveChanges();
        }

        [WebInvoke(Method = "POST")]
        public void addNote(Note newNote, WSSDAEntities sourse)
        {
            WSSDAEntities context = sourse;

            try
            {
                context.Note.Add(newNote);
            }
            catch(Exception e)
            {

            }
            context.SaveChanges();
        }

        /*[WebInvoke(Method = "POST")]
        public void addNote(Note newNote)
        {

        }*/
    }
}
