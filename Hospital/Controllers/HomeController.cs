using Job.Data.Models.Domain;
using Job.Services.IService;
using Job.Services.Models;
using Job.Services.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Job.Controllers
{
    public class HomeController : Controller
    {
        private IJobService JobService;
        public IUserService UserService;

        public HomeController()
        {
            JobService = new JobService();
            UserService = new UserService();
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            App_User app_User = UserService.GetUserData(userId);
            Session["Data"] = app_User;
            if (userId != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: PostJob/Create
        [HttpPost]
        public ActionResult PostJob(PostJobDto postJobDto)
        {
            try
            {
                JobService.AddJob(postJobDto, "mo");
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                var test = ex.Message;
                return View();
            }
        }
    }
}