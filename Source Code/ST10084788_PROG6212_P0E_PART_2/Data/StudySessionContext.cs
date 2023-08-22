using ST10084788_PROG6212_P0E_PART_2.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10084788_PROG6212_P0E_PART_2.Data
{
    internal class StudySessionContext : DbContext
    {
        public DbSet<StudySession> StudySessionInfo { get; set; }
    }
}
