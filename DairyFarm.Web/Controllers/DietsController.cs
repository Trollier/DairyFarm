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
    public class DietsController : Controller
    {
        // private DairyFarmEntities db = new DairyFarmEntities();
        private readonly IDairyFarmService _dairyFarmService;

        public DietsController(IDairyFarmService dairyFarmService)
        {
            _dairyFarmService = dairyFarmService;
        }
        // GET: Diets
        public ActionResult Index(string message, int? state)
        {
            var diets = _dairyFarmService.GetDiets();
            return View(diets.ToList());
        }

        // GET: Diets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diet diet = _dairyFarmService.GetDietById(id);
            if (diet == null)
            {
                return HttpNotFound();
            }
            return View(diet);
        }

        // GET: Diets/Create
        public ActionResult Create()
        {
            ViewBag.IdSeason = new SelectList(_dairyFarmService.GetSeasons(), "IdSeason", "Label");
            ViewBag.IdFood = new SelectList(_dairyFarmService.GetFoods(), "IdFood", "Label");
            ViewBag.IdCattleType = new SelectList(_dairyFarmService.GetCattleTypes(), "IdCattleType", "Label");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Diet diet)
        {
            if (ModelState.IsValid)
            {
                
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Regime bien ajouté"
                };
                if (_dairyFarmService.AddDiet(diet) == false)
                {
                    return RedirectToAction("Index", "Foods", new { message = "Erreur dans l'ajout", state = 0 });
                }
                return RedirectToAction("Index", "Foods", new { message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "Foods", new { message = "Erreur dans l'ajout", state = 0 });
        }

        // GET: Diets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diet diet = _dairyFarmService.GetDietById(id);
            if (diet == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSeason = new SelectList(_dairyFarmService.GetSeasons(), "IdSeason", "Label", diet.IdSeason);
            return View(diet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Diet diet)
        {
            if (ModelState.IsValid)
            {
                var popup = new MessageInfo
               {
                   State = 1,
                   Message = "Nourriture bien édité"
               };
                if (_dairyFarmService.EditDiet(diet) == false)
                {
                    return RedirectToAction("Index", "Foods", new { message = "Erreur dans l'édition", state = 0 });
                }
                return RedirectToAction("Index", "Foods", new { id = popup.Id, message = popup.Message, state = popup.State });

            }
            return RedirectToAction("Index", "Foods", new { message = "Erreur dans l'édition", state = 0 });
        }

        // GET: Diets/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Diet diet = db.Diets.Find(id);
        //    if (diet == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(diet);
        //}

        //// POST: Diets/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Diet diet = db.Diets.Find(id);
        //    db.Diets.Remove(diet);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}
