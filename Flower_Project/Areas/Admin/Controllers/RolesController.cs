using System;
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


        public ActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ProcessCreateRole(string roleName, string description)
        {
            var role = new Role()
            {
                Name = roleName,
                Description = description,
                CreatedAt = DateTime.Now,
                Status = Role.RoleStatus.Active
            };
            var result = _roleManager.Create(role);
            ViewBag.Result = result;
            return Redirect("/Admin/Admin");
        }
    }
}