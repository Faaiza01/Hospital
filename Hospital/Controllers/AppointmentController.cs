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
    public class AppointmentController : Controller
    {
        public IHospitalService HospitalService;
        public IUserService UserService;


        public AppointmentController()
        {
            HospitalService = new HospitalService();
            UserService = new UserService();

        }

        // GET: Appointment
        public ActionResult Index()
        {
            var identityId = User.Identity.GetUserId();

            var user = UserService.GetUserData(identityId);
            ViewBag.appointments = HospitalService.GetPatientAppointmentHistory(user.UserId);
            return View(ViewBag.appointments);
        }

        public ActionResult CancelAppointment(int appointmentId)
        {
            HospitalService.CancelAppointment(appointmentId);
            return RedirectToAction("Index");
        }

        public ActionResult AppointmentDetail()
        {
            ViewBag.appointmentsDetails = HospitalService.AppointmentDetail();
            return View(ViewBag.appointmentsDetails);
        }

    }
}