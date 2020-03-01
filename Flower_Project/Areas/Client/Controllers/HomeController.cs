using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flower_Project.Areas.Admin.Models;

namespace Flower_Project.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext db = new MyDbContext();
        public ActionResult ByCategory(string id)
        {
            ViewData["category"] = db.Categories.Find(id);
            var listProducts = db.Products.Where(s => s.CategoryId == id).ToList();
            return View("Products", listProducts);
        }
        // GET: Client/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductDetail()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View(db.Products.ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}