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
    public class CandelsController : Controller
    {
        private Ali_AmirEntities db = new Ali_AmirEntities();
        
        // GET: Candels
        public ActionResult Index()
        {
            return View(db.Candels.ToList());
        }
        

        // GET: Candels/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candel candel = db.Candels.Find(id);
            if (candel == null)
            {
                return HttpNotFound();
            }
            return View(candel);
        }

        // GET: Candels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Candels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Candel candel)
        {
            if (ModelState.IsValid)
            {
                db.Candels.Add(candel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(candel);
        }

        // GET: Candels/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candel candel = db.Candels.Find(id);
            if (candel == null)
            {
                return HttpNotFound();
            }
            return View(candel);
        }

        // POST: Candels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Candel candel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candel);
        }

        // GET: Candels/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candel candel = db.Candels.Find(id);
            if (candel == null)
            {
                return HttpNotFound();
            }
            return View(candel);
        }

        // POST: Candels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Candel candel = db.Candels.Find(id);
            db.Candels.Remove(candel);
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
