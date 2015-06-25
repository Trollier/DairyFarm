using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DairyFarm.Core.DAL;
using DairyFarm.Service;
using DairyFarm.Web.Models;

namespace DairyFarm.Web.Controllers
{
    public class MealsController : Controller
    {
        //private DairyFarmEntities db = new DairyFarmEntities();
        private readonly IDairyFarmService _dairyFarmService;

        public MealsController(IDairyFarmService dairyFarmService)
        {
            _dairyFarmService = dairyFarmService;
        }
        // GET: Meals
        public ActionResult Index(string message, int? state)
        {
            var meals = _dairyFarmService.GetMeals();
            if (message != null)
            {
                ViewBag.Message = message;
                ViewBag.State = state;
            }
            return View(meals.ToList());
        }

        // GET: Meals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = _dairyFarmService.GetMealById(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // GET: Meals/Create
        public ActionResult Create(int id)
        {
            var meal = new Meal();
            var diet = new Diet();
            diet = _dairyFarmService.getDietByDate(DateTime.Now, id);
            meal.DateMeal = DateTime.Now;

            meal.IdHerd = id;
            meal.FoodExhausted = new List<Food>();
            meal.givenFood = new List<Meal>();
            foreach (var VARIABLE in _dairyFarmService.FoodExhausted() )
            {
                meal.FoodExhausted.Add(VARIABLE);
            }
            foreach (var meals in _dairyFarmService.GivenFood(DateTime.Now))
            {
                meal.givenFood.Add(meals);
            }
            ViewBag.Hours = new SelectList(Util.Hours);
            ViewBag.IdFood = new SelectList(diet.Foods.Where(f=>f.TotQuantity>0).ToList(), "IdFood", "Label");
            return View(meal);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Meal meal)
        {
            if (ModelState.IsValid)
            {
                meal.HourMeal = TimeSpan.Parse(meal.Hours);
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Repas bien ajouté"
                };
                if (_dairyFarmService.AddMeal(meal) == false)
                {
                    return RedirectToAction("Index", "Cattle", new { message = "Erreur dans l'ajout", state = 0 });
                }
                return RedirectToAction("Index", "Cattle", new { message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "Cattle", new { message = "Erreur dans l'ajout", state = 0 });
        }

        // GET: Meals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = _dairyFarmService.GetMealById(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdFood = new SelectList(_dairyFarmService.GetFoods(), "IdFood", "Label", meal.IdFood);
            ViewBag.Hours = new SelectList(Util.Hours);

            return View(meal);
        }


        [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult Edit(Meal meal)
        {
            if (ModelState.IsValid)
            {
                var editMeal = _dairyFarmService.GetMealById(meal.IdMeal);
                var difference = meal.Quantity - editMeal.Quantity;
                editMeal.Food.TotQuantity -= difference;
                editMeal.Quantity = meal.Quantity;
                editMeal.IdFood = meal.IdFood;
                editMeal.DateMeal = meal.DateMeal;
                editMeal.HourMeal = TimeSpan.Parse(meal.Hours);
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Repas bien édité"
                };
                if (_dairyFarmService.EditMeal(editMeal) == false)
                {
                    return RedirectToAction("Index", "Meals", new { message = "Erreur dans l'édition", state = 0 });
                }
                return RedirectToAction("Index", "Meals", new { id = popup.Id, message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "Meals", new { message = "Erreur dans l'édition", state = 0 });
        }

        //// GET: Meals/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Meal meal = db.Meals.Find(id);
        //    if (meal == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(meal);
        //}

        //// POST: Meals/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Meal meal = db.Meals.Find(id);
        //    db.Meals.Remove(meal);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}
