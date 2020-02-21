using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Flower_Project.App_Start;
using Flower_Project.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Flower_Project.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private MyDbContext _dbContext;

        private IdentityConfig.ApplicationUserManager _applicationUserManager;

        public UsersController(MyDbContext dbContext, IdentityConfig.ApplicationUserManager applicationUserManager)
        {
            _dbContext = dbContext;
            _applicationUserManager = applicationUserManager;
        }


        // GET: Admin/Users
        public ActionResult Register()
        {
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> ProcessRegister(string fullName,
            string userName, string password, string address, 
            string phoneNumber,

            string email)

        {
            var user = new User()
            {
                FullName =  fullName,
                UserName = userName,
                PhoneNumber = phoneNumber,
                Email = email,
                Address = address,
                Birthday = DateTime.Now,
                CreatedAt = DateTime.Now,
                Status = Models.User.MemberStatus.Active
            };

            var result = await _applicationUserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                _applicationUserManager.AddToRole(user.Id, "User");
                return Redirect("/Admin/Users/Login");

            }
            return Redirect("/Admin/Users/Register");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessLogin(string userName, string password)
        {
            var user = _applicationUserManager.Find(userName, password);

            if (user == null)
            {
                return HttpNotFound("sai tk hoac mk!");
            }

            var ident = _applicationUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignIn(new AuthenticationProperties{IsPersistent = false}, ident);
            ViewBag.GetName = HttpContext.User.Identity.GetUserId();
            return Redirect("/Admin/Admin");
        }
        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/Admin/Admin");
        }
    }
}