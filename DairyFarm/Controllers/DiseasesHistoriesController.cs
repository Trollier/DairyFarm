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
    public class DiseasesHistoriesController : Controller
    {
        private DairyFarmEntities _db = new DairyFarmEntities();

        // GET: DiseasesHistories
        public ActionResult Index(int id)
        {
            var diseasesHistories = _db.DiseasesHistories.Where(d=>d.EndDate!= null).Include(d => d.Cattle).Include(d => d.Disease);
            var cattle = _db.Cattles.Find(id);
            ViewBag.codeCattle = cattle.CodeCattle;
            ViewBag.idCattle = cattle.IdCattle;
            return View(diseasesHistories.ToList());
        }

        // GET: DiseasesHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiseasesHistory diseasesHistory = _db.DiseasesHistories.Find(id);
            if (diseasesHistory == null)
            {
                return HttpNotFound();
            }
            return View(diseasesHistory);
        }

        // GET: DiseasesHistories/Create
        public ActionResult Create(int id)
        {
            DiseasesHistory diseaseHistory = new DiseasesHistory();
            diseaseHistory.IdCattle = id;
            diseaseHistory.StartDate = DateTime.Now;
            ViewBag.IdMedicalTreatments = new SelectList(_db.MedicalTreatments, "IdTreatment", "Label");
            ViewBag.IdDisease = new SelectList(_db.Diseases, "IdDisease", "Label");
            return PartialView("_New_DiseasesHistory",diseaseHistory);
        }

        // POST: DiseasesHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DiseasesHistory diseasesHistory)
        {
            if (ModelState.IsValid)
            {
                foreach (var idTreatment in diseasesHistory.IdMedicalTreatments)
                {
                    var medic = _db.MedicalTreatments.Find(idTreatment);
                    diseasesHistory.MedicalTreatments.Add(medic);
                }
                _db.DiseasesHistories.Add(diseasesHistory);
                _db.SaveChanges();
                return RedirectToAction("Details", "Cattle", new { id = diseasesHistory.IdCattle });
            }

            return RedirectToAction("Details", "Cattle", new { id = diseasesHistory.IdCattle });
        }

        // GET: DiseasesHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiseasesHistory diseasesHistory = _db.DiseasesHistories.Find(id);
            if (diseasesHistory == null)
            {
                return HttpNotFound();
            }
            diseasesHistory.IdMedicalTreatments = new List<int>();
            foreach (var treatment in diseasesHistory.MedicalTreatments)
            {
                //var medic = _db.MedicalTreatments.Find(treatment);
                diseasesHistory.IdMedicalTreatments.Add(treatment.IdTreatment);
            }
            ViewBag.IdMedicalTreatments = new SelectList(_db.MedicalTreatments, "IdTreatment", "Label",diseasesHistory.IdMedicalTreatments.ToArray());
            ViewBag.IdDisease = new SelectList(_db.Diseases, "IdDisease", "Label",diseasesHistory.IdDisease);
            return PartialView("_EditDiseasesHistory", diseasesHistory);
        }

        // POST: DiseasesHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( DiseasesHistory diseasesHistory)
        {
            if (ModelState.IsValid)
            {
                if (diseasesHistory.IdMedicalTreatments != null)
                {
                    diseasesHistory.MedicalTreatments = new List<MedicalTreatment>();
                    //diseasesHistory.MedicalTreatments.Clear();
                    foreach (var idTreatment in diseasesHistory.IdMedicalTreatments)
                    {
                        var medic = _db.MedicalTreatments.Find(idTreatment);
                        diseasesHistory.MedicalTreatments.Add(medic);
                    }
                }

                var dH = _db.DiseasesHistories.Find(diseasesHistory.IdDiseasesHistory);
                dH.MedicalTreatments.Clear();
                dH.IdDisease = diseasesHistory.IdDisease;
                dH.MedicalTreatments = diseasesHistory.MedicalTreatments;
                dH.StartDate = diseasesHistory.StartDate;
                dH.EndDate = diseasesHistory.EndDate;
                dH.VeterinaryVisit = diseasesHistory.VeterinaryVisit;

                _db.Entry(dH).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Details", "Cattle", new { id = diseasesHistory.IdCattle });
            }
            ViewBag.IdCattle = new SelectList(_db.Cattles, "IdCattle", "CodeCattle", diseasesHistory.IdCattle);
            ViewBag.IdDisease = new SelectList(_db.Diseases, "IdDisease", "Label", diseasesHistory.IdDisease);
            return RedirectToAction("Details", "Cattle", new { id = diseasesHistory.IdCattle });
        }

        // GET: DiseasesHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiseasesHistory diseasesHistory = _db.DiseasesHistories.Find(id);
            if (diseasesHistory == null)
            {
                return HttpNotFound();
            }
            return View(diseasesHistory);
        }

        // POST: DiseasesHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiseasesHistory diseasesHistory = _db.DiseasesHistories.Find(id);
            _db.DiseasesHistories.Remove(diseasesHistory);
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
