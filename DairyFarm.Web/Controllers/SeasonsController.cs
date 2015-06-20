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

namespace DairyFarm.Controllers
{
    public class SeasonsController : Controller
    {
        //private DairyFarmEntities db = new DairyFarmEntities();
        private readonly IDairyFarmService _dairyFarmService;

        public SeasonsController(IDairyFarmService dairyFarmService)
        {
            _dairyFarmService = dairyFarmService;
        }
        // GET: Seasons
        public ActionResult Index(string message, int? state)
        {
            if (message != null)
            {
                ViewBag.Message = message;
                ViewBag.State = state;
            }
            return View(_dairyFarmService.GetSeasons());
        }

        // GET: Seasons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Season season = _dairyFarmService.GetSeasonById(id);
            if (season == null)
            {
                return HttpNotFound();
            }
            return View(season);
        }

        // GET: Seasons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seasons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Season season)
        {
            if (ModelState.IsValid)
            {
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Saison bien ajouté"
                };
                if (_dairyFarmService.AddSeason(season) == false)
                {
                    return RedirectToAction("Index", "Seasons", new { message = "Erreur dans l'ajout", state = 0 });
                }
                return RedirectToAction("Index", "Seasons", new { message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "Seasons", new { message = "Erreur dans l'ajout", state = 0 });
        }

        // GET: Seasons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Season season = _dairyFarmService.GetSeasonById(id);
            if (season == null)
            {
                return HttpNotFound();
            }
            return View(season);
        }

        // POST: Seasons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Season season)
        {
            if (ModelState.IsValid)
            {
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Saison bien édité"
                };
                if (_dairyFarmService.EditSeason(season) == false)
                {
                    return RedirectToAction("Index", "Seasons", new { message = "Erreur dans l'édition", state = 0 });
                }
                return RedirectToAction("Index", "Seasons", new {  message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "Seasons", new {  message = "Erreur dans l'édition", state = 0 });
        }

        // GET: Seasons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Season season = _dairyFarmService.GetSeasonById(id);
            if (season == null)
            {
                return HttpNotFound();
            }
            return View(season);
        }

        // POST: Seasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var popup = new MessageInfo
            {
              State = 1,
                Message = "Supprimé",
               
            };
            _dairyFarmService.DeleteFood(id);
            return RedirectToAction("Index", "Seasons", new { message = popup.Message, state = popup.State });
        }

    }
}
