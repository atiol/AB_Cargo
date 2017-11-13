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
    public class CitiesController : Controller
    {
        private AB_CargoEntities db = new AB_CargoEntities();

        // GET: Cities
        public ActionResult Index()
        {
            return View(db.LOCATIONs.ToList());
        }

        // GET: Cities/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOCATION lOCATION = db.LOCATIONs.Find(id);
            if (lOCATION == null)
            {
                return HttpNotFound();
            }
            return View(lOCATION);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CITY_ID, CITY_NAME")] LOCATION lOCATION)
        {
            if (ModelState.IsValid)
            {
                db.LOCATIONs.Add(lOCATION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOCATION);
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOCATION lOCATION = db.LOCATIONs.Find(id);
            if (lOCATION == null)
            {
                return HttpNotFound();
            }
            return View(lOCATION);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CITY_ID,CITY_NAME")] LOCATION lOCATION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOCATION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOCATION);
        }

        // GET: Cities/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOCATION lOCATION = db.LOCATIONs.Find(id);
            if (lOCATION == null)
            {
                return HttpNotFound();
            }
            return View(lOCATION);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            LOCATION lOCATION = db.LOCATIONs.Find(id);
            db.LOCATIONs.Remove(lOCATION);
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
