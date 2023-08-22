using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10084788_PROG6212_P0E_PART_2.Model
{
    internal class Login
    {
        // Variables for login table
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }


    }
}
