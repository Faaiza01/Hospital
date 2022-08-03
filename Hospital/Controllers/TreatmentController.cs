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
    public class TreatmentController : Controller
    {
        private IUserService UserService;

        public TreatmentController()
        {
            UserService = new UserService();
        }
        // GET: Treatment
        public ActionResult Index()
        {
            var identityId = User.Identity.GetUserId();

            var user = UserService.GetUserData(identityId);
            ViewBag.perscriptions = UserService.GetTreatmentList(user.UserId);
            return View(ViewBag.perscriptions);
        }

        public ActionResult TreatmentList()
        {
            ViewBag.allTreatment = UserService.GetAllTreatmentList();
            return View(ViewBag.allTreatment);

        }


    }
}