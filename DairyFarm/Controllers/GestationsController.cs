using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DairyFarm.DAL;

namespace DairyFarm.Controllers
{
    public class GestationsController : Controller
    {
        private DairyFarmEntities db = new DairyFarmEntities();

        // GET: Gestations
        public ActionResult Index(int id)
        {
            var gestations = db.Gestations.Where(g => g.EndDateGestation != null);
            var cattle = db.Cattles.Find(id);
            ViewBag.codeCattle =  cattle.CodeCattle;
            ViewBag.idCattle = cattle.IdCattle;
            return View(gestations);
        }

        // GET: Gestations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestation gestation = db.Gestations.Find(id);
            if (gestation == null)
            {
                return HttpNotFound();
            }
            return View(gestation);
        }

        // GET: Gestations/Create
        public ActionResult Create()
        {
            ViewBag.IdCattle = new SelectList(db.Cattles, "IdCattle", "CodeCattle");
            return View();
        }

        // POST: Gestations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Gestation gestation)
        {
            if (ModelState.IsValid)
            {
                db.Gestations.Add(gestation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCattle = new SelectList(db.Cattles, "IdCattle", "CodeCattle", gestation.IdCattle);
            return View(gestation);
        }

        //get gestation for partial view
        public ActionResult NewGestation(int id)
        {
            var model = new Gestation
            {
                IdCattle = id,
                StartDateGestation = DateTime.Now.Date
            };
            return PartialView("_NewGestation_Form",model);
        }

        // GET: Gestations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestation gestation = db.Gestations.Find(id);
            if (gestation == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCattle = new SelectList(db.Cattles, "IdCattle", "CodeCattle", gestation.IdCattle);
            return View(gestation);
        }

        // POST: Gestations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gestation gestation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gestation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Cattle", new { id = gestation.IdCattle });
            }
            return RedirectToAction("Details", "Cattle", new { id = gestation.IdCattle });

        }

        // GET: Gestations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestation gestation = db.Gestations.Find(id);
            if (gestation == null)
            {
                return HttpNotFound();
            }
            return View(gestation);
        }

        // POST: Gestations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gestation gestation = db.Gestations.Find(id);
            db.Gestations.Remove(gestation);
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
