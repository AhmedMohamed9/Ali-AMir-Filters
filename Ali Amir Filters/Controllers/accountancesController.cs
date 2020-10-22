using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ali_Amir_Filters.Models;
using Ali_Amir_Filters.Models.ViewModel;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace Ali_Amir_Filters.Controllers
{
    public class accountancesController : Controller
    {
        private Ali_AmirEntities db = new Ali_AmirEntities();

        // GET: accountances
        public ActionResult Index()
        {
            return View(db.accountances.ToList().OrderBy(x => x.accountance_date));
        }

        // GET: accountances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            accountance accountance = db.accountances.Find(id);
            if (accountance == null)
            {
                return HttpNotFound();
            }
            return View(accountance);
        }

        // GET: accountances/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: accountances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,accountance_date,type,money")] accountance accountance)
        {
            if (ModelState.IsValid)
            {
                db.accountances.Add(accountance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountance);
        }

        // GET: accountances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            accountance accountance = db.accountances.Find(id);
            if (accountance == null)
            {
                return HttpNotFound();
            }
            return View(accountance);
        }

        // POST: accountances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,accountance_date,type,money")] accountance accountance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountance);
        }

        // GET: accountances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            accountance accountance = db.accountances.Find(id);
            if (accountance == null)
            {
                return HttpNotFound();
            }
            return View(accountance);
        }

        // POST: accountances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            accountance accountance = db.accountances.Find(id);
            db.accountances.Remove(accountance);
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
        public ActionResult accountance_summar(DateTime dd)
        {
            if (dd==null)
            {
                return HttpNotFound();
            }
            
            decimal? moneyout = db.accountance_summary(dd.Month, dd.Year, "out").SingleOrDefault();
            decimal? moneyin = db.accountance_summary(dd.Month, dd.Year, "in").SingleOrDefault();
            decimal? moneycomm = db.accountance_summary(dd.Month, dd.Year, "comm").SingleOrDefault();

            decimal mout = moneyout == null ? 0 : (decimal) moneyout;
            decimal min = moneyin == null ? 0 :(decimal) moneyin;
            decimal mcomm = moneycomm == null ? 0 :(decimal) moneycomm;

            Accountance_summary acc = new Accountance_summary
            {
                acc_date = dd,
                money_out = mout,
                money_in = min,
                money_commission = mcomm,
                money_profit = min - (mout + mcomm)
                
            };
            return View("acc_summary", acc);
        }
    }
}
