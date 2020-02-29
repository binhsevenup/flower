using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
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
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View(_dbContext.Users.ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProcessRegister(UserClass userClass)

        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FullName = userClass.FullName, Address = userClass.Address, UserName = userClass.UserName,
                    Email = userClass.Email, PhoneNumber = userClass.PhoneNumber, CreatedAt = DateTime.Now,
                    UpdatedAt = userClass.UpdatedAt, DeletedAt = userClass.DeletedAt,
                    Status = Models.User.MemberStatus.Active
                };

//
                var result = await _applicationUserManager.CreateAsync(user, userClass.Password);
                if (result.Succeeded)
                {
                    _applicationUserManager.AddToRole(user.Id, "User");
                    return Redirect("/Admin/Users/Login");

                }
            }

            return Redirect("/Admin/Users/Create");
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
            authManager.SignIn(new AuthenticationProperties {IsPersistent = false}, ident);
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
//
//        public ActionResult Edit(string id)
//        {
//
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//
//            var user = _dbContext.Users.Find(id);
//            if (user == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
//            }
//
//            return View(user);
//
//
//        }
//
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit( UserClass userClass)
//        {
//           
//            if ( user.Id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//
//            }
//
//            var existUser = _dbContext.Users.Find(user.Id);
//
////            if (existProduct == null || existProduct.IsDeleted())
////            {
////                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
////
////            }
//            if (ModelState.IsValid)
//            {
//                existUser.FullName = userClass.FullName;
//                existUser.UserName = userClass.UserName;
//                existUser.Address = userClass.Address;
//                existUser.PhoneNumber = userClass.PhoneNumber;
//                existUser.UpdatedAt = DateTime.Now;
//                existUser.Status = (User.MemberStatus) userClass.Status;
//              
//                _dbContext.Users.AddOrUpdate(existUser);
//                _dbContext.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(user);
//        }
    }
}