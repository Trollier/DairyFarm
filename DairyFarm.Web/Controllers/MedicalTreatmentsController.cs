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
    [Authorize]
    public class MedicalTreatmentsController : Controller
    {
        //private DairyFarmEntities db = new DairyFarmEntities();
        private readonly IDairyFarmService _dairyFarmService;

        public MedicalTreatmentsController(IDairyFarmService dairyFarmService)
        {
            _dairyFarmService = dairyFarmService;
        }
        // GET: MedicalTreatments
        public ActionResult Index(string message, int? state)
        {
            if (message != null)
            {
                ViewBag.Message = message;
                ViewBag.State = state;
            }
            return View(_dairyFarmService.GetMedicalTreatments());
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
        public ActionResult Create( MedicalTreatment medicalTreatment)
        {
            if (ModelState.IsValid)
            {

                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Traitement bien ajouté"
                };
                if (_dairyFarmService.AddMedicalTreatment(medicalTreatment) == false)
                {
                    return RedirectToAction("Index", "MedicalTreatments", new { message = "Erreur dans l'ajout", state = 0 });
                }
                return RedirectToAction("Index", "MedicalTreatments", new { message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "MedicalTreatments", new { message = "Erreur dans l'ajout", state = 0 });
        }

        // GET: MedicalTreatments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalTreatment medicalTreatment = _dairyFarmService.GetMedicalTreatmentById(id);
            if (medicalTreatment == null)
            {
                return HttpNotFound();
            }
            return View(medicalTreatment);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( MedicalTreatment medicalTreatment)
        {
            if (ModelState.IsValid)
            {
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Traitement bien édité"
                };
                if (_dairyFarmService.EditMedicalTreatment(medicalTreatment) == false)
                {
                    return RedirectToAction("Index", "MedicalTreatments", new { message = "Erreur dans l'édition", state = 0 });
                }
                return RedirectToAction("Index", "MedicalTreatments", new { id = popup.Id, message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "MedicalTreatments", new { message = "Erreur dans l'édition", state = 0 });
        }

        // GET: MedicalTreatments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalTreatment medicalTreatment = _dairyFarmService.GetMedicalTreatmentById(id);
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
            var popup = new MessageInfo
            {
                State = 1,
                Message = "Supprimé"
            };
            _dairyFarmService.DeleteFood(id);
            return RedirectToAction("Index", "MedicalTreatments", new { message = popup.Message, state = popup.State });
        }


    }
}
