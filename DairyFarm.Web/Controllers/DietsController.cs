using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
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
            ViewBag.IdFoods = new SelectList(_dairyFarmService.GetFoods(), "IdFood", "Label");
            ViewBag.IdCattleTypes = new SelectList(_dairyFarmService.GetCattleTypes(), "IdCattleType", "Label");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Diet diet)
        {
            if (ModelState.IsValid)
            {
                foreach (var idFood in diet.IdFoods)
                {
                    diet.Foods.Add(_dairyFarmService.GetFoodById(idFood));
                }
                foreach (var idCattleType in diet.IdCattleTypes)
                {
                    diet.CattleTypes.Add(_dairyFarmService.GetCattleTypeById(idCattleType));
                }
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Regime bien ajouté"
                };
                if (_dairyFarmService.AddDiet(diet) == false)
                {
                    return RedirectToAction("Index", "Diets", new { message = "Erreur dans l'ajout", state = 0 });
                }
                return RedirectToAction("Index", "Diets", new { message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "Diets", new { message = "Erreur dans l'ajout", state = 0 });
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
            ViewBag.IdFoods = new MultiSelectList(_dairyFarmService.GetFoods(), "IdFood", "Label", diet.Foods.Select(m => m.IdFood).ToArray());
            ViewBag.IdCattleTypes = new MultiSelectList(_dairyFarmService.GetCattleTypes(), "IdCattleType", "Label", diet.CattleTypes.Select(m => m.IdCattleType).ToArray());
            return View(diet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Diet diet)
        {
            if (ModelState.IsValid)
            {
                var dietEdit = _dairyFarmService.GetDietById(diet.IdDiet);
                dietEdit.Foods.Clear();
                dietEdit.CattleTypes.Clear();
                foreach (var idFood in diet.IdFoods)
                {
                    dietEdit.Foods.Add(_dairyFarmService.GetFoodById(idFood));
                }
                foreach (var idCattleType in diet.IdCattleTypes)
                {
                    dietEdit.CattleTypes.Add(_dairyFarmService.GetCattleTypeById(idCattleType));
                }

                dietEdit.IdSeason = diet.IdSeason;
                dietEdit.Label = diet.Label;
                
                var popup = new MessageInfo
               {
                   State = 1,
                   Message = "Nourriture bien édité"
               };
                if (_dairyFarmService.EditDiet(dietEdit) == false)
                {
                    return RedirectToAction("Index", "Diets", new { message = "Erreur dans l'édition", state = 0 });
                }
                return RedirectToAction("Index", "Diets", new { id = popup.Id, message = popup.Message, state = popup.State });

            }
            return RedirectToAction("Index", "Diets", new { message = "Erreur dans l'édition", state = 0 });
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
