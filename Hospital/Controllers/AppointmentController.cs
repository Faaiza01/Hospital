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
    public class AppointmentController : Controller
    {
        public IJobService JobService;
        public IUserService UserService;


        public AppointmentController()
        {
            JobService = new JobService();
            UserService = new UserService();

        }

        // GET: Appointment
        public ActionResult Index()
        {
            var identityId = User.Identity.GetUserId();

            var user = UserService.GetUserData(identityId);
            ViewBag.appointments = JobService.GetPatientAppointmentHistory(user.UserId);
            return View(ViewBag.appointments);
        }

        public ActionResult CancelAppointment(int appointmentId)
        {
            JobService.CancelAppointment(appointmentId);
            return RedirectToAction("Index");
        }

        public ActionResult AppointmentDetail()
        {
            ViewBag.appointmentsDetails = JobService.AppointmentDetail();
            return View(ViewBag.appointmentsDetails);
        }

    }
}