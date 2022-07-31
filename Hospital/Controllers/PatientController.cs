using Hospital.Data;
using Hospital.Data.Models.Domain;
using Hospital.Services.IService;
using Hospital.Services.Service;
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
        private IHospitalService HospitalService;
        private IUserService UserService;


        public PatientController()
        {
            HospitalService = new HospitalService();
            UserService = new UserService();

        }
        // GET: Patient
        public ActionResult Index()
        {
            var model = new Hospital.Data.BookAppointmentDto();
            var data = HospitalService.GetDoctorByDepartment();
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
                HospitalService.BookAppointment(appointment, Users.UserId);
                return RedirectToAction("Index", "Appointment");
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
                var model = new Hospital.Data.BookAppointmentDto();
                var data  = HospitalService.GetDoctorByDepartment(id);
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
            var fee = HospitalService.GetDoctorsFee(id);

            return Json(fee, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrescriptionList()
        {
            var identityId = User.Identity.GetUserId();

            var user = UserService.GetUserData(identityId);
            ViewBag.Pperscriptions = UserService.GetPatientPrescriptionList(user.UserId);
            return View(ViewBag.Pperscriptions);

        }



    }
}