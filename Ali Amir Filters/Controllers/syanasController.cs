using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ali_Amir_Filters.Models;
using Ali_Amir_Filters.Models.ViewModel;
using Microsoft.Ajax.Utilities;

namespace Ali_Amir_Filters.Controllers
{
    public class syanasController : Controller
    {
        private Ali_AmirEntities db = new Ali_AmirEntities();

        // GET: syanas
        public ActionResult Index()
        {
            //var syanas = db.syanas.Include(s => s.Coustmer).Where(x => x.is_done == false).OrderBy(x => x.date_syana);
            //return View(syanas.ToList());
            var tab = db.syana_table3().ToList();
            foreach (var item in tab)
            {
                item.candls = "";
                var cn = db.syanas.Where(x => x.date_syana == item.date_syana && x.customer_id == item.Id && x.is_done == false).Select(x => x.candel_name).ToList();
                foreach (var item2 in cn)
                {
                    item.candls += item2 + " ";
                }
            }

            return View(tab);
        }

        public ActionResult search(DateTime bydate)
        {
            var tab = db.syana_table3().Where(x => x.date_syana.Month == bydate.Month && x.date_syana.Year == bydate.Year).ToList();
            foreach (var item in tab)
            {
                item.candls = "";
                var cn = db.syanas.Where(x => x.date_syana == item.date_syana && x.customer_id == item.Id).Select(x => x.candel_name).ToList();
                foreach (var item2 in cn)
                {
                    item.candls += item2 + " ";
                }
            }
            return View("Index", tab);
        }
        //
        public ActionResult searchbyname(string name)
        {
            //var 

            var tab = db.syana_table3()
                .Where(x => x.Name.Contains(name)
                         || x.phone_Number.Contains(name)
                         || x.area.Contains(name)
                         || x.adress.Contains(name)
                         ).ToList();
            //if (tab.Count==0)
            //{
            //   var tab2 = db.syana_table3().Where(x => x.phone_Number2.Contains(name)).FirstOrDefault();
            //}
            
            foreach (var item in tab)
            {
                item.candls = "";
                var cn = db.syanas.Where(x => x.date_syana == item.date_syana && x.customer_id == item.Id).Select(x => x.candel_name).ToList();
                foreach (var item2 in cn)
                {
                    item.candls += item2 + " ";
                }
            }
            return View("Index", tab);
        }

        // GET: syanas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            syana syana = db.syanas.Find(id);
            if (syana == null)
            {
                return HttpNotFound();
            }
            return View(syana);
        }

        // GET: syanas/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.Coustmers, "Id", "Name");
            return View();
        }

        // POST: syanas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,date_syana,customer_id,candel_name,filter_name")] syana syana)
        {
            if (ModelState.IsValid)
            {
                db.syanas.Add(syana);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.Coustmers, "Id", "Name", syana.customer_id);
            return View(syana);
        }

        // GET: syanas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            syana syana = db.syanas.Find(id);
            if (syana == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.Coustmers, "Id", "Name", syana.customer_id);
            return View(syana);
        }

        // POST: syanas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(syana syana)
        {
            if (ModelState.IsValid)
            {
                db.Entry(syana).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.Coustmers, "Id", "Name", syana.customer_id);
            return View(syana);
        }

        // GET: syanas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            syana syana = db.syanas.Find(id);
            if (syana == null)
            {
                return HttpNotFound();
            }
            return View(syana);
        }

        // POST: syanas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            syana syana = db.syanas.Find(id);
            db.syanas.Remove(syana);
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

        public ActionResult det(DateTime date, int? id)
        {
            if (id == null || date == null)
            {
                return HttpNotFound();
            }
            var syanas = db.syanas.Where(x => x.date_syana.Month == date.Month && x.date_syana.Year == date.Year && x.customer_id == id && x.is_done == false).ToList();

            if (syanas.Count > 0)
            {
                Session["coustmer_name"] = syanas.Select(x => x.Coustmer.Name).FirstOrDefault().ToString();
                Session["date"] = syanas.Select(x => x.date_syana).FirstOrDefault().ToString("yyyy/MM/dd");
                Session["id"] = syanas.Select(x => x.customer_id).FirstOrDefault();
                Session["month"] = syanas.Select(s => s.date_syana).FirstOrDefault().Month;
                Session["year"] = syanas.Select(s => s.date_syana).FirstOrDefault().Year;
            }
            return View(syanas);
        }
        public ActionResult is_done(int id, int cous_id, DateTime dd)
        {
            syana syana = db.syanas.Find(id);
            //add syana to history
            #region add syana history 
            Syana_history sn_history = new Syana_history
            {
                date_syana_done = DateTime.Now,
                coustmer_id = cous_id,
                candel_name = syana.candel_name,
                month_late = (int)syana.months_late
            };
            db.Syana_history.Add(sn_history);
            #endregion


            syana.date_syana = syana.date_syana.AddMonths(Convert.ToInt32(syana.number_of_months));
            syana.is_done = false;
            syana.months_late = 0;
            db.Entry(syana).State = EntityState.Modified;
            db.SaveChanges();

            accountance acc = new accountance
            {
                accountance_date = DateTime.Now,
                type = "out",
                money = db.Candels.Where(x => x.Name == syana.candel_name).Select(s => s.price).First(),
                coustmer_id = syana.customer_id
            };
            db.accountances.Add(acc);
            db.SaveChanges();
            return RedirectToAction("det", new { @date = dd, @id = cous_id });
        }
        public ActionResult late_1month(int id, int cous_id, DateTime dd)
        {
            syana syana = db.syanas.Find(id);
            syana.months_late += 1;
            syana.date_syana = syana.date_syana.AddMonths(1);

            db.SaveChanges();
            return RedirectToAction("det", new { @date = dd, @id = cous_id });
        }
        public ActionResult late_3month(int id, int cous_id, DateTime dd)
        {
            syana syana = db.syanas.Find(id);
            syana.months_late += 3;
            syana.date_syana = syana.date_syana.AddMonths(3);
            db.SaveChanges();
            return RedirectToAction("det", new { @date = dd, @id = cous_id });
        }
        public ActionResult addmoney(decimal money, string type, int cous_id, DateTime dd)
        {

            accountance acc = new accountance
            {
                accountance_date = DateTime.Now,
                type = type,
                money = money,
                coustmer_id = cous_id

            };
            db.accountances.Add(acc);
            db.SaveChanges();
            return RedirectToAction("det", new { @date = dd, @id = cous_id });
        }

    }
}
