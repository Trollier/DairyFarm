using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DairyFarm.Core.DAL;

namespace DairyFarm.Web.Controllers
{
    public class CattleProductionsController : Controller
    {
        private readonly DairyFarmEntities _db = new DairyFarmEntities();

        // GET: CattleProductions
        public ActionResult Index()
        {
            var cattleProductions = _db.CattleProductions.Include(c => c.Cattle);
            return View(cattleProductions.ToList());
        }

        // GET: CattleProductions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CattleProduction cattleProduction = _db.CattleProductions.Find(id);
            if (cattleProduction == null)
            {
                return HttpNotFound();
            }
            return View(cattleProduction);
        }

        // GET: CattleProductions/Create
        public ActionResult SetProductions()
        {
            ICollection<CattleProduction> cattleProductions = new List<CattleProduction>();

            foreach (var cattle in _db.Cattles.Where(c => c.Herd.IdCattleType == 3))
            {
                cattleProductions.Add(new CattleProduction
                {
                    IdCattle = cattle.IdCattle,
                    Quantity = 0,
                    // hourprod = "",
                    Cattle = cattle
                });
            }

            ViewBag.message = " Le " + DateTime.Now.ToString("dddd dd MM yyyy");
            return View(cattleProductions);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetProductions(ICollection<CattleProduction> cattleProductions)
        {
            if (ModelState.IsValid)
            {
                foreach (var production in cattleProductions)
                {
                    _db.CattleProductions.Add(production);
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.message = " Le " + DateTime.Now.ToString("dddd dd MM yyyy");
            return View(cattleProductions);
        }


        // GET: CattleProductions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CattleProduction cattleProduction = _db.CattleProductions.Find(id);
            if (cattleProduction == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCattle = new SelectList(_db.Cattles, "IdCattle", "CodeCattle", cattleProduction.IdCattle);
            return View(cattleProduction);
        }

        // POST: CattleProductions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduction,IdCattle,Dateprod,hourprod,quantity")] CattleProduction cattleProduction)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cattleProduction).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCattle = new SelectList(_db.Cattles, "IdCattle", "CodeCattle", cattleProduction.IdCattle);
            return View(cattleProduction);
        }

        // GET: CattleProductions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CattleProduction cattleProduction = _db.CattleProductions.Find(id);
            if (cattleProduction == null)
            {
                return HttpNotFound();
            }
            return View(cattleProduction);
        }

        // POST: CattleProductions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CattleProduction cattleProduction = _db.CattleProductions.Find(id);
            _db.CattleProductions.Remove(cattleProduction);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
