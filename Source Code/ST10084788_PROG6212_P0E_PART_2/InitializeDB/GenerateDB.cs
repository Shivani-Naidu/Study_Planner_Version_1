using Microsoft.SqlServer.Server;
using ST10084788_PROG6212_P0E_PART_2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace ST10084788_PROG6212_P0E_PART_2.InitializeDB
{
    internal class GenerateDB
    {
        // contains methods that converts items inside of the database to a list
        public async Task LoginDB ()
        { 
        
            //LoginContext logContext = new LoginContext();
            using (var context = new LoginContext())
            {
                var loginDB = from rec in context.User
                                  select rec;
                var results = loginDB.ToList();
               
            }
        }

        public async Task ModuleDB()
        {
            //ModuleContext modContext = new ModuleContext();
           
            using (var context = new ModuleContext())
            {
                var moduleDB = from rec in context.ModuleInfo
                                  select rec;
                var results = moduleDB.ToList();

            }
        }

        public async Task SemesterDB()
        {
            //SemesterContext modContext = new SemesterContext();
           
            using (var context = new SemesterContext())
            {
                var semesterDB = from rec in context.SemesterInfo
                                  select rec;
                var results = semesterDB.ToList();

            }
        }

        public async Task StudySessionDB()
        {
            //SemesterContext modContext = new SemesterContext();

            using (var context = new StudySessionContext())
            {
                var studySessionDB = from rec in context.StudySessionInfo
                                 select rec;
                var results = studySessionDB.ToList();

            }
        }



    }
}


   
