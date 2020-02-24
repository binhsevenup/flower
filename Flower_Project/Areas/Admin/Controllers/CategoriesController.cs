using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Flower_Project.Areas.Admin.Models;
using Flower_Project.Models;
using PagedList;

namespace Flower_Project.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Admin/Categories
        public ActionResult Index(string sortOrder, string currentFilter, string search, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSort = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSort = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.CreatedSort = sortOrder == "Created" ? "created_desc" : "Created";
            ViewBag.UpdatedSort = sortOrder == "Updated" ? "updated_desc" : "Updated";
            ViewBag.StatusSort = sortOrder == "Status" ? "status_desc" : "Status";

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            var categories = from s in db.Categories select s;

            if (!String.IsNullOrEmpty(search))
            {
                categories = categories.Where(s => s.CategoryId.Contains(search)
                                                   || s.CategoryName.Contains(search));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    categories = categories.OrderByDescending(s => s.CategoryId);
                    break;
                case "Name":
                    categories = categories.OrderBy(s => s.CategoryName);
                    break;
                case "name_desc":
                    categories = categories.OrderByDescending(s => s.CategoryName);
                    break;
                case "Created":
                    categories = categories.OrderBy(s => s.CreatedAt);
                    break;
                case "created_desc":
                    categories = categories.OrderByDescending(s => s.CreatedAt);
                    break;
                case "Updated":
                    categories = categories.OrderBy(s => s.UpdatedAt);
                    break;
                case "updated_desc":
                    categories = categories.OrderByDescending(s => s.UpdatedAt);
                    break;
                case "Status":
                    categories = categories.OrderBy(s => s.Status);
                    break;
                case "status_desc":
                    categories = categories.OrderByDescending(s => s.Status);
                    break;
                default:
                    categories = categories.OrderBy(s => s.CategoryId);
                    break;

            }

            int limit = 10;
            int pageNumber = (page ?? 1);
            

            return View(categories.Where(s => s.Status == Category.CategoryStatus.Active).ToPagedList(pageNumber, limit));
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null || category.IsDeleted())
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName,CreatedAt,UpdatedAt,DeletedAt,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedAt = DateTime.Now;
                category.Status = Category.CategoryStatus.Active;
                db.Categories.Add(category);
                db.SaveChanges();
                return Redirect("/Admin/Categories/Index");
            }

            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName,CreatedAt,UpdatedAt,DeletedAt,Status")] Category category)
        {
            if (category.CategoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var existCategory = db.Categories.Find(category.CategoryId);

            if (existCategory == null || existCategory.IsDeleted())
            {
                return HttpNotFound();

            }
            if (ModelState.IsValid)
            {
                existCategory.CategoryId = category.CategoryId;
                existCategory.CategoryName = category.CategoryName;
                existCategory.UpdatedAt = DateTime.Now;
                existCategory.Status = category.Status;
                db.Categories.AddOrUpdate(existCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Categories.Find(id);
            if (category == null || category.IsDeleted())
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var existCategory = db.Categories.Find(id);

            if (existCategory == null || existCategory.IsDeleted())
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            }

            if (ModelState.IsValid)
            {
                existCategory.DeletedAt = DateTime.Now;
                existCategory.Status = Category.CategoryStatus.Deleted;
                db.Categories.AddOrUpdate(existCategory);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
