using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Data.Models.Domain;

namespace Job.Data.Repository
{
    public class JobContext: DbContext
    {
        public JobContext() : base("JobContext")
        {
            Database.SetInitializer(new JobInitialiser());
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
    }
}
