using Hospital.Services.IService;
using Hospital.Services.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class PrescriptionController : Controller
    {
        private IUserService UserService;

        public PrescriptionController()
        {
            UserService = new UserService();
        }
        // GET: Prescription
        public ActionResult Index()
        {
            var identityId = User.Identity.GetUserId();

            var user = UserService.GetUserData(identityId);
            ViewBag.perscriptions = UserService.GetPrescriptionList(user.UserId);
            return View(ViewBag.perscriptions);
        }

        public ActionResult PrescriptionList()
        {
            ViewBag.allPrescription = UserService.GetAllPrescriptionList();
            return View(ViewBag.allPrescription);

        }


    }
}