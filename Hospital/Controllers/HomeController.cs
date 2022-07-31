using Hospital.Data.Models.Domain;
using Hospital.Models;
using Hospital.Services.IService;
using Hospital.Services.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IHospitalService HospitalService;
        public IUserService UserService;

        public HomeController()
        {
            HospitalService = new HospitalService();
            UserService = new UserService();
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            UserService = new UserService();
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public async Task<ActionResult> Index()
        
        {
            var user = new RegisterViewModel
            {
                FirstName = "Admin",
                LastName = "Admin",
                Password = "Admin123@",
                ConfirmPassword = "Admin123@",
                Email = "admin@gmail.com"
            };
            var admin = UserService.GetUsers().Where(x => x.Role == "Admin").FirstOrDefault();
            if (admin == null)
                await RegisterAdmin(user);
            var userId = User.Identity.GetUserId();
            Users Users = UserService.GetUserData(userId);
            Session["Data"] = Users;
            if (userId != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        public async Task<ActionResult> RegisterAdmin(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    Users app_User = new Users
                    {
                        IdentityId = user.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Role = "Admin"
                    };
                    UserService = new UserService();
                    UserService.AddUser(app_User);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            return View(model);
        }
     

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}