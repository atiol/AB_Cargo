using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AB_CargoServices.Models;

namespace AB_CargoServices.Controllers
{
    public class InquiriesController : Controller
    {
        private AB_CargoEntities db = new AB_CargoEntities();

        // GET: Inquiries
        public ActionResult Index()
        {
            ViewBag.Inquiries = db.USER_MESSAGES.Count();
            return View(db.USER_MESSAGES.ToList());
        }

        // GET: Inquiries/Details/5
        public ActionResult Details(decimal Id)
        {
            if (Id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_MESSAGES inquiry = db.USER_MESSAGES.Find(Id);
            if (inquiry == null)
            {
                return HttpNotFound();
            }
            return View(inquiry);
        }

        // GET: Inquiries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inquiries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SENT,NAME,EMAIL,MESSAGE")] USER_MESSAGES Inquiry)
        {
            if (ModelState.IsValid)
            {
                db.USER_MESSAGES.Add(Inquiry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Inquiry);
        }

        // GET: Inquiries/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_MESSAGES inquiry = db.USER_MESSAGES.Find(id);
            if (inquiry == null)
            {
                return HttpNotFound();
            }
            return View(inquiry);
        }

        // POST: Inquiries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,EMAIL,MESSAGE")] USER_MESSAGES inquiry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inquiry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inquiry);
        }

        // GET: Inquiries/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_MESSAGES inquiry = db.USER_MESSAGES.Find(id);
            if (inquiry == null)
            {
                return HttpNotFound();
            }
            return View(inquiry);
        }

        // POST: Inquiries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal Id)
        {
            USER_MESSAGES Inquiry = db.USER_MESSAGES.Find(Id);
            db.USER_MESSAGES.Remove(Inquiry);
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
