using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DairyFarm.Core.DAL;
using DairyFarm.Service;
using DairyFarm.Web.Models;

namespace DairyFarm.Web.Controllers
{
    [Authorize]
    public class FoodsController : Controller
    {
        //private DairyFarmEntities db = new DairyFarmEntities();
        private readonly IDairyFarmService _dairyFarmService;

        public FoodsController(IDairyFarmService dairyFarmService)
        {
            _dairyFarmService = dairyFarmService;
        }
        // GET: Foods
        public ActionResult Index(string message, int? state)
        {
            if (message != null)
            {
                ViewBag.Message = message;
                ViewBag.State = state;
            }
            return View(_dairyFarmService.GetFoods());
        }

        // GET: Foods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = _dairyFarmService.GetFoodById(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Foods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Food food)
        {
            if (ModelState.IsValid)
            {
               
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Nourriture bien ajouté"
                };
                 if (_dairyFarmService.AddFood(food)==false)
                {
                    return RedirectToAction("Index", "Foods", new { message = "Erreur dans l'ajout", state = 0 });
                }
                return RedirectToAction("Index", "Foods", new {  message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "Foods", new {  message = "Erreur dans l'ajout", state = 0 });
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = _dairyFarmService.GetFoodById(id);

            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Food food)
        {
            if (ModelState.IsValid)
            {
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Nourriture bien édité"
                };
                if (_dairyFarmService.EditFood(food) == false)
                {
                    return RedirectToAction("Index", "Foods", new { message = "Erreur dans l'édition", state = 0 });
                }
                return RedirectToAction("Index", "Foods", new { id = popup.Id, message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "Foods", new {  message = "Erreur dans l'édition", state = 0 });
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = _dairyFarmService.GetFoodById(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Delete/5
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
            return RedirectToAction("Index", "Foods", new {  message = popup.Message, state = popup.State });
        }

   
    }
}
