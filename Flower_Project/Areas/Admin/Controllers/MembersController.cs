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

namespace Flower_Project.Areas.Admin.Controllers
{
    public class MembersController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Admin/Members
        public ActionResult Index()
        {
            return View(db.Members.ToList());
        }

        // GET: Admin/Members/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Admin/Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberId,MemberName,Address,PhoneNumber,Email,CreatedAt,UpdatedAt,DeletedAt,Status")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.CreatedAt = DateTime.Now;
                member.UpdatedAt = DateTime.Now;
                member.DeletedAt = DateTime.Now;
                member.Status = Member.MemberStatus.Active;
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Admin/Members/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(member);
        }

        // POST: Admin/Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,MemberName,Address,PhoneNumber,Email,CreatedAt,UpdatedAt,DeletedAt,Status")] Member member)
        {
            if (member == null || member.MemberId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var existMember = db.Members.Find(member.MemberId);

            if (existMember == null || existMember.IsDeleted())
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            }
            if (ModelState.IsValid)
            {
                existMember.MemberId = member.MemberId;
                existMember.MemberName = member.MemberName;
                existMember.Email = member.Email;
                existMember.Address = member.Address;
                existMember.PhoneNumber = member.PhoneNumber;
                existMember.UpdatedAt = DateTime.Now;

                db.Members.AddOrUpdate(existMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Admin/Members/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(member);
        }

        // POST: Admin/Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var existMember = db.Members.Find(id);
            if (existMember == null || existMember.IsDeleted())
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            }

            existMember.Status = Member.MemberStatus.Deleted;
            existMember.DeletedAt = DateTime.Now;
            db.SaveChanges();
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
