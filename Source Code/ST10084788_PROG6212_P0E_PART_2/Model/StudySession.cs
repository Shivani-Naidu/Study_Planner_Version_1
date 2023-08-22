using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ST10084788_PROG6212_P0E_PART_2.Model
{
    internal class StudySession
    {
        // Variables for StudySession table
        [Key, Column(Order = 0)]
        public int StudyId { get; set; }
        
        [Key, Column(Order = 1)]
        public string ModuleCode { get; set; }
        
        [Key, Column(Order = 2)]
        public string SemesterName { get; set; }
        
        [Key, Column(Order = 3)]
        public string Username { get; set; }
        public DateTime StudyDate { get; set; }
        public int HoursStudied { get; set; }
        public int StudyWeek { get; set; }

    }
}
