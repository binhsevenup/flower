using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flower_Project.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Flower_Project.Areas.Admin.Controllers
{
    public class RolesController : Controller
    {
        private MyDbContext dbContext = new MyDbContext();

        private RoleManager<Role> _roleManager;

        public RolesController()
        {
            RoleStore<Role> roleStore = new RoleStore<Role>(dbContext);
            _roleManager = new RoleManager<Role>(roleStore);
        }
        // GET: Admin/Roles

        public ActionResult Index()
        {
            return View(dbContext.IdentityRoles.ToList());
        }



        public ActionResult CreateRole()
        {
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessCreateRole(RoleClass roleClass)
        {
            if (ModelState.IsValid)
            {
                var role = new Role
                {
                    Name = roleClass.Name, Description = roleClass.Description, CreatedAt = DateTime.Now,
                    UpdatedAt = roleClass.UpdatedAt, DeletedAt = roleClass.DeletedAt, Status = Role.RoleStatus.Active
                };
                var result = _roleManager.Create(role);
                if (result.Succeeded)
                {
                    ViewBag.Result = result;
                    return Redirect("/Admin/Admin");

                }

            }

            return View("Create");

            

            




        }
    }
}