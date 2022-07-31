using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Data.Models.Domain;
using Hospital.Data.DAO;
using Hospital.Data.IDAO;

namespace Hospital.Data.Repository
{
    public class HospitalInitialiser :
        System.Data.Entity.DropCreateDatabaseIfModelChanges<HospitalContext>
    {
        protected override void Seed(HospitalContext context)
        {
            //Employer employer1 = new Employer()
            //{ 
            //    HospitalTitle = "Software Engineer", 
            //    HospitalDescription = "Full Stack Developer",
            //    HospitalCategory = "Permanent",
            //    Salary = "50000",
            //    CompanyName = "Telenor",
            //    ComapanyEmail = "mo@mo.com",
            //    CompanyAddress = "XYZ",
            //    CompanyWebsite = "mo@mo.com",
            //};
            //context.Employers.Add(employer1);

            //Users appUser1 = new Users()
            //{
            //    FirstName = "Faaiza",
            //    LastName = "Rashid",
            //    Email = "faaizarashid@hotmail.com",
            //    Role = "Admin",
            //};
            //context.Users.Add(appUser1);

            context.SaveChanges();
        }
    }
}
