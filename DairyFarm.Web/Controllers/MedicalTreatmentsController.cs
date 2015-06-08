using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DairyFarm.Core.DAL;

namespace DairyFarm.Web.Controllers
{
    public class MedicalTreatmentsController : Controller
    {
        private DairyFarmEntities db = new DairyFarmEntities();

        // GET: MedicalTreatments
        public ActionResult Index()
        {
            return View(db.MedicalTreatments.ToList());
        }

        // GET: MedicalTreatments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalTreatment medicalTreatment = db.MedicalTreatments.Find(id);
            if (medicalTreatment == null)
            {
                return HttpNotFound();
            }
            return View(medicalTreatment);
        }

        // GET: MedicalTreatments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicalTreatments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTreatment,Label")] MedicalTreatment medicalTreatment)
        {
            if (ModelState.IsValid)
            {
                db.MedicalTreatments.Add(medicalTreatment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medicalTreatment);
        }

        // GET: MedicalTreatments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalTreatment medicalTreatment = db.MedicalTreatments.Find(id);
            if (medicalTreatment == null)
            {
                return HttpNotFound();
            }
            return View(medicalTreatment);
        }

        // POST: MedicalTreatments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTreatment,Label")] MedicalTreatment medicalTreatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalTreatment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicalTreatment);
        }

        // GET: MedicalTreatments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalTreatment medicalTreatment = db.MedicalTreatments.Find(id);
            if (medicalTreatment == null)
            {
                return HttpNotFound();
            }
            return View(medicalTreatment);
        }

        // POST: MedicalTreatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicalTreatment medicalTreatment = db.MedicalTreatments.Find(id);
            db.MedicalTreatments.Remove(medicalTreatment);
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
