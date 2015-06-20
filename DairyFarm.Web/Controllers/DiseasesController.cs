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
    public class DiseasesController : Controller
    {
       // private DairyFarmEntities _db = new DairyFarmEntities();
        private readonly IDairyFarmService _dairyFarmService;

        public DiseasesController(IDairyFarmService dairyFarmService)
        {
            _dairyFarmService = dairyFarmService;
        }
        // GET: Diseases
        public ActionResult Index(string message, int? state)
        {
            if (message != null)
            {
                ViewBag.Message = message;
                ViewBag.State = state;
            }
            return View(_dairyFarmService.GetDiseases());
        }

        // GET: Diseases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = _dairyFarmService.GetDiseaseById(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        
        public ActionResult Create()
        {
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Disease disease)
        {
            if (ModelState.IsValid)
            {

                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Maladie bien ajouté"
                };
                if (_dairyFarmService.AddDisease(disease) == false)
                {
                    return RedirectToAction("Index", "Diseases", new { message = "Erreur dans l'ajout", state = 0 });
                }
                return RedirectToAction("Index", "Diseases", new { message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "Diseases", new { message = "Erreur dans l'ajout", state = 0 });
        }

        // GET: Diseases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = _dairyFarmService.GetDiseaseById(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Disease disease)
        {
            if (ModelState.IsValid)
            {
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Maladie bien édité"
                };
                if (_dairyFarmService.EditDisease(disease) == false)
                {
                    return RedirectToAction("Index", "Diseases", new { message = "Erreur dans l'édition", state = 0 });
                }
                return RedirectToAction("Index", "Diseases", new { id = popup.Id, message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "Diseases", new { message = "Erreur dans l'édition", state = 0 });
        }

        // GET: Diseases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = _dairyFarmService.GetDiseaseById(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        // POST: Diseases/Delete/5
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
            return RedirectToAction("Index", "Diseases", new { message = popup.Message, state = popup.State });
        }


    }
}
