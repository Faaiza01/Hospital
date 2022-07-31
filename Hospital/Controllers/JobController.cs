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
    public abstract class HospitalController : Controller
    {
       public IUserService UserService;
        public IHospitalService HospitalService;

        protected HospitalController()
        {
            UserService = new UserService();
            HospitalService = new HospitalService();

        }
    }
}