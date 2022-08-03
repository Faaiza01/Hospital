using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Data.Models.Domain;

namespace Hospital.Data.Repository
{
    public class HospitalContext: DbContext
    {
        public HospitalContext() : base("HospitalContext")
        {
            Database.SetInitializer(new HospitalInitialiser());
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Treatment> Treatment { get; set; }
    }
}
