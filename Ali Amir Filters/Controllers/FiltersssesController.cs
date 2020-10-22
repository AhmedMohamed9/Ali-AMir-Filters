using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ali_Amir_Filters.Models;

namespace Ali_Amir_Filters.Controllers
{
    public class FiltersssesController : Controller
    {
        private Ali_AmirEntities db = new Ali_AmirEntities();

        // GET: Filterssses
        public ActionResult Index()
        {
            return View(db.Filterssses.ToList());
        }

        // GET: Filterssses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filtersss filtersss = db.Filterssses.Find(id);
            if (filtersss == null)
            {
                return HttpNotFound();
            }
            return View(filtersss);
        }

        // GET: Filterssses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Filterssses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Filtersss filtersss)
        {
            if (ModelState.IsValid)
            {
                db.Filterssses.Add(filtersss);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(filtersss);
        }

        // GET: Filterssses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filtersss filtersss = db.Filterssses.Find(id);
            if (filtersss == null)
            {
                return HttpNotFound();
            }
            return View(filtersss);
        }

        // POST: Filterssses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Filtersss filtersss)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filtersss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(filtersss);
        }

        // GET: Filterssses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filtersss filtersss = db.Filterssses.Find(id);
            if (filtersss == null)
            {
                return HttpNotFound();
            }
            return View(filtersss);
        }

        // POST: Filterssses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Filtersss filtersss = db.Filterssses.Find(id);
            db.Filterssses.Remove(filtersss);
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
