using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalWebApi.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Login Page";
            return View();
        }
    }
}
