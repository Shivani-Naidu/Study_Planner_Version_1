using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10084788_PROG6212_P0E_PART_2.Model
{
    internal class Semester
    {
        // Variables for semester table
        [Key, Column (Order = 0)]
        public string SemesterName { get; set; }

        [Key, Column(Order = 1)]
        public string Username { get; set; }


       
        public int SemesterWeeks { get; set; }
        public DateTime SemesterDate { get; set; }

      
    }
}
