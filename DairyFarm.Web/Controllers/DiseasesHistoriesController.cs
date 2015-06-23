using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DairyFarm.Core.DAL;
using DairyFarm.Service;
using DairyFarm.Web.Models;

namespace DairyFarm.Web.Controllers
{
    public class DiseasesHistoriesController : Controller
    {
        private DairyFarmEntities _db = new DairyFarmEntities();
        private readonly IDairyFarmService _dairyFarmService;

        public DiseasesHistoriesController (IDairyFarmService dairyFarmService)
        {
            _dairyFarmService = dairyFarmService;
        }
        // GET: DiseasesHistories
        public ActionResult Index(int id)
        {
            var diseasesHistories = _db.DiseasesHistories.Where(d => d.EndDate != null && d.IdCattle==id).Include(d => d.Cattle).Include(d => d.Disease);
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
            return PartialView("_New_DiseasesHistory", diseaseHistory);
        }

        // POST: DiseasesHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DiseasesHistory diseasesHistory)
        {
            var popup = new MessageInfo
            {
                Message = "Erreur dans l'ajout",
                State = 0
            };
            if (ModelState.IsValid)
            {
                if (_dairyFarmService.GetDiseaseById(diseasesHistory.IdDisease).Contagious == true)
                {
                    _dairyFarmService.GetCattleById(diseasesHistory.IdCattle).InQuarantine = true;
                } 
                foreach (var idTreatment in diseasesHistory.IdMedicalTreatments)
                {
                    var medic = _dairyFarmService.GetMedicalTreatmentById(idTreatment);
                    diseasesHistory.MedicalTreatments.Add(medic);
                }
                if (_dairyFarmService.AddDiseasesHistory(diseasesHistory))
                {
                    popup.Message = "Maladie Bien ajouté";
                    popup.State = 1;
                    return RedirectToAction("Details", "Cattle", new { id = diseasesHistory.IdCattle, message = popup.Message, state = popup.State });
                }
            }
            return RedirectToAction("Details", "Cattle", new { id = diseasesHistory.IdCattle, message = popup.Message, state = popup.State });
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
            ViewBag.IdMedicalTreatments = new MultiSelectList(_db.MedicalTreatments, "IdTreatment", "Label", diseasesHistory.MedicalTreatments.Select(m => m.IdTreatment).ToArray());
            ViewBag.IdDisease = new SelectList(_db.Diseases, "IdDisease", "Label", diseasesHistory.IdDisease);
            return PartialView("_EditDiseasesHistory", diseasesHistory);
        }

        // POST: DiseasesHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DiseasesHistory diseasesHistory)
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
