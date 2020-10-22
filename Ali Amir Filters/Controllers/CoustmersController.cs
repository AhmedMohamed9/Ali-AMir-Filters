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
    public class CoustmersController : Controller
    {
        private Ali_AmirEntities db = new Ali_AmirEntities();

        // GET: Coustmers
        public ActionResult Index()
        {
            var coustmers = db.Coustmers.Include(c => c.Filtersss);
            return View(coustmers.ToList());

        }

        public ActionResult syana_history(int id, string cous_name)
        {
            var tab = db.sp_history6(id).ToList();

            ViewBag.name = cous_name;
            foreach (var item in tab)
            {
                item.type = "";
                var cn = db.Syana_history.Where(x => x.date_syana_done == item.date_syana_done && x.coustmer_id == item.id).Select(x => x.candel_name).ToList();
                foreach (var item2 in cn)
                {
                    item.type += item2 + " ";
                }
            }
            return View("syana_history", tab);

        }


        public ActionResult search(string name)
        {
            var cous = db.Coustmers
                .Where(x => x.phone_Number.Contains(name) || x.phone_Number2.Contains(name) || x.Name.Contains(name) || x.area.Contains( name));
            return View("Index", cous.ToList());
        }

        // GET: Coustmers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coustmer coustmer = db.Coustmers.Find(id);
            if (coustmer == null)
            {
                return HttpNotFound();
            }
            return View(coustmer);
        }

        // GET: Coustmers/Create
        public ActionResult Create()
        {
            ViewBag.Filter_id = new SelectList(db.Filterssses, "id", "name");
            return View();
        }

        // POST: Coustmers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Coustmer coustmer)
        {
            if (ModelState.IsValid)
            {
                var filtermonths = (from month in db.get_monthss(coustmer.Filter_id)
                                    select month);
                foreach (var item in filtermonths)
                {
                    syana sn = new syana
                    {
                        date_syana = coustmer.Dates.AddMonths(item.Candel_date),
                        customer_id = coustmer.Id,
                        candel_name = item.Candel_name,
                        filter_name = item.filter_name,
                        number_of_months = item.Candel_date,
                        is_done = false,
                        months_late = 0

                    };
                    detect_months(ref sn, item.Candel_date);
                    db.syanas.Add(sn);
                }
                #region Add coustmer
                db.Coustmers.Add(coustmer);
                db.SaveChanges();
                #endregion
                return RedirectToAction("Index");
            }

            ViewBag.Filter_id = new SelectList(db.Filterssses, "id", "name", coustmer.Filter_id);
            return View(coustmer);
        }
        public void detect_months(ref syana sn, int months_of_candel, int plus = 0)
        {

            if (sn.date_syana.Year < DateTime.Now.Year)
            {
                plus = plus + months_of_candel;
                sn.date_syana = sn.date_syana.AddMonths(months_of_candel);
                detect_months(ref sn, months_of_candel, plus);
            }
            else
            {
                if (!(sn.date_syana.Year > DateTime.Now.Year) && sn.date_syana.Month < DateTime.Now.Month)
                {
                    plus = plus + months_of_candel;
                    sn.date_syana = sn.date_syana.AddMonths(months_of_candel);
                    detect_months(ref sn, months_of_candel, plus);
                }
            }

        }

        // GET: Coustmers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coustmer coustmer = db.Coustmers.Find(id);
            if (coustmer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Filter_id = new SelectList(db.Filterssses, "id", "name", coustmer.Filter_id);
            Session["fid"] = coustmer.Filter_id;
            Session["dat"] = coustmer.Dates;
            return View(coustmer);
        }

        // POST: Coustmers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Coustmer coustmer)
        {
            if (ModelState.IsValid)
            {
                coustmer.Filter_id = int.Parse(Session["fid"].ToString());
                coustmer.Dates = (DateTime)Session["dat"];
                db.Entry(coustmer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Filter_id = new SelectList(db.Filterssses, "id", "name", coustmer.Filter_id);
            return View(coustmer);
        }

        // GET: Coustmers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coustmer coustmer = db.Coustmers.Find(id);
            if (coustmer == null)
            {
                return HttpNotFound();
            }
            return View(coustmer);
        }

        // POST: Coustmers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coustmer coustmer = db.Coustmers.Find(id);
            db.Coustmers.Remove(coustmer);
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
