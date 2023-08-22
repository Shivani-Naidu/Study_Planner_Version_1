using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterClassLibrary
{
    public class Class1
    {
    }

    // Semester class that contains variables for the semester details
    public class SemesterClass
    {
        public string SemesterName { get; set; }
        public int SemesterWeeks { get; set; }
        public DateTime SemesterStartDate { get; set; }

        public SemesterClass() { }
        public SemesterClass (string SemesterName, int SemesterWeeks, DateTime SemesterStartDate)
        {
            this.SemesterName = SemesterName;
            this.SemesterWeeks = SemesterWeeks;
            this.SemesterStartDate = SemesterStartDate;
        }
    }

    public class ModuleClass
    { 
        public string ModuleName { get; set; }  
        public string ModuleCode { get; set; }
        public int ModuleCredits { get; set; }
        public int ClassHoursPerWeek { get; set;}

        public ModuleClass() { }

        public ModuleClass (string ModuleName, string ModuleCode, int ModuleCredits, int MlassHoursPerWeek)
        {
            this.ModuleName = ModuleName;
            this.ModuleCode = ModuleCode;
            this.ModuleCredits = ModuleCredits;
            this.ClassHoursPerWeek = ClassHoursPerWeek;
        }   
    }

    public class Calculate
    {
        public  int CalcHoursPerWeek(int credits, int weeks, int classHrs)
        {

            // calculates how many required study hours per week
            int HrsPerWeek;
            HrsPerWeek = (int)Math.Round(((double)(credits * 10) / weeks) - classHrs, MidpointRounding.AwayFromZero);
            return HrsPerWeek;
        }
        public int currentWeek(DateTime startDate)
        {
            //calculates current week of semester
            int currentWeek;
            currentWeek = (int)Math.Ceiling(((DateTime.Now - startDate).TotalDays + 1) / 7);
            return currentWeek;
        }
        public  int workWeek(DateTime workDate, DateTime startDate)
        {
            // calculates the week in which work was done
            int workWeek;
            workWeek = (int)Math.Ceiling(((workDate - startDate).TotalDays + 1) / 7);
            return workWeek;
        }  
    }
}
