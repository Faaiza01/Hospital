using Job.Data;
using Job.Data.Models.Domain;
using Job.Services.IService;
using Job.Services.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forest.Controllers
{
    public class PatientController : Controller
    {
        private IJobService JobService;
        private IUserService UserService;


        public PatientController()
        {
            JobService = new JobService();
            UserService = new UserService();

        }
        // GET: Patient
        public ActionResult Index()
        {
            var model = new Job.Data.BookAppointmentDto();
            var data = JobService.GetDoctorByDepartment();
            model.DoctorsData = GetSelectListItems(data);
            ViewData["DoctorsData"] = GetSelectListItems(data);

            return View();
        }

        [HttpPost]
        public ActionResult BookAppointment(BookAppointmentDto appointment)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                Users Users = UserService.GetUserData(userId);
                JobService.BookAppointment(appointment, Users.UserId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(string id)
        {
            try
            {
                var model = new Job.Data.BookAppointmentDto();
                var data  = JobService.GetDoctorByDepartment(id);
                model.DoctorsData = GetSelectListItems(data);
                ViewData["DoctorsData"] = model.DoctorsData;
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        private IEnumerable<SelectListItem> GetSelectListItems(List<Users> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.UserId.ToString(),
                    Text = element.FirstName
                });
            }
            return selectList;
        }

        public JsonResult GetFee(string id)
        {
            var fee = JobService.GetDoctorsFee(id);

            return Json(fee, JsonRequestBehavior.AllowGet);
        }



    }
}