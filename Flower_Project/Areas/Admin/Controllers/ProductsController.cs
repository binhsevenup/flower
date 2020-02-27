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
    public class ProductsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Admin/Products
        public ActionResult Index(string sortOrder, string currentFilter, string search, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSort = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSort = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.NameCategorySort = sortOrder == "NameCategory" ? "name_category_desc" : "NameCategory";
            ViewBag.QuantitySort = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewBag.PriceSort = sortOrder == "Price" ? "price_desc" : "Price";
//            ViewBag.PriceSaleSort = sortOrder == "PriceSale" ? "price_sale_desc" : "PriceSale";
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

            var product = from s in db.Products select s;

            if (!String.IsNullOrEmpty(search))
            {
                product = product.Where(s => s.Category.CategoryName.Contains(search)
                                                   || s.ProductName.Contains(search)||
                                                   s.ProductId.Contains(search));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    product = product.OrderByDescending(s => s.ProductId);
                    break;
                case "Name":
                    product = product.OrderBy(s => s.ProductName);
                    break;
                case "name_desc":
                    product = product.OrderByDescending(s => s.ProductName);
                    break;
                case "NameCategory":
                    product = product.OrderBy(s => s.CategoryId);
                    break;
                case "name_category_desc":
                    product = product.OrderByDescending(s => s.CategoryId);
                    break;
                case "Quantity":
                    product = product.OrderBy(s => s.Quantity);
                    break;
                case "quantity_desc":
                    product = product.OrderByDescending(s => s.Quantity);
                    break;
                case "Price":
                    product = product.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    product = product.OrderByDescending(s => s.Price);
                    break;
//                case "PriceSale":
//                    product = product.OrderBy(s => s.PriceSale);
//                    break;
//                case "price_sale_desc":
//                    product = product.OrderByDescending(s => s.PriceSale);
//                    break;
                case "Created":
                    product = product.OrderBy(s => s.CreatedAt);
                    break;
                case "created_desc":
                    product = product.OrderByDescending(s => s.CreatedAt);
                    break;
                case "Updated":
                    product = product.OrderBy(s => s.UpdatedAt);
                    break;
                case "updated_desc":
                    product = product.OrderByDescending(s => s.UpdatedAt);
                    break;
                case "Status":
                    product = product.OrderBy(s => s.Status);
                    break;
                case "status_desc":
                    product = product.OrderByDescending(s => s.Status);
                    break;
                default:
                    product = product.OrderBy(s => s.ProductId);
                    break;

            }


            int limit = 10;
            int pageNumber = (page ?? 1);
            var products = db.Products.Include(p => p.Category);
            return View(product.Where(s => s.Status == Product.ProductStatus.Active).ToPagedList(pageNumber, limit));
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null || product.IsDeleted())
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Quantity,Price,Description,Detail,Avatar,CategoryId,CreatedAt,UpdatedAt,DeletedAt,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Status = Product.ProductStatus.Active;
                product.CreatedAt = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Quantity,Price,Description,Detail,Avatar,CategoryId,CreatedAt,UpdatedAt,DeletedAt,Status")] Product product)
        {
            if (product == null || product.ProductId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var existProduct = db.Products.Find(product.ProductId);

            if (existProduct == null || existProduct.IsDeleted())
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            }
            if (ModelState.IsValid)
            {
                existProduct.ProductId = product.ProductId;
                existProduct.ProductName = product.ProductName;
                existProduct.Category = product.Category;
                existProduct.CategoryId = product.CategoryId;
                existProduct.Description = product.Description;
                existProduct.Price = product.Price;
                existProduct.Avatar = product.Avatar;
                existProduct.Quantity = product.Quantity;
                existProduct.Detail = product.Detail;
                existProduct.UpdatedAt = DateTime.Now;
                existProduct.Status = product.Status;
                db.Products.AddOrUpdate(existProduct);                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var existProduct = db.Products.Find(id);

            if (existProduct == null || existProduct.IsDeleted())
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            }

            if (ModelState.IsValid)
            {
                existProduct.DeletedAt = DateTime.Now;
                existProduct.Status = Product.ProductStatus.Deleted;
                db.Products.AddOrUpdate(existProduct);
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
