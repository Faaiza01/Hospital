using Job.Data.Models.Domain;
using Job.Models;
using Job.Services.IService;
using Job.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forest.Controllers
{
    public class DoctorController : Controller
    {
        private IUserService UserService;

        public DoctorController()
        {
            UserService = new UserService();
        }
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }


        public void AddDoctor(RegisterViewModel model, ApplicationUser user)
        {
            App_User app_User = new App_User
            {
                IdentityId = user.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                ContactNumber = model.ContactNumber,
                Role = "Doctor",
                Gender = model.Gender,
                Specialization = model.Specialization,
                ConsultancyFee = model.ConsultancyFee
            };
            UserService.AddUser(app_User);
        }
    }
}