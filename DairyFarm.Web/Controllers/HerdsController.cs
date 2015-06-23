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
    public class HerdsController : Controller
    {
        //private DairyFarmEntities _db = new DairyFarmEntities();
        private readonly IDairyFarmService _dairyFarmService;

        public HerdsController(IDairyFarmService dairyFarmService)
        {
            _dairyFarmService = dairyFarmService;
        }
        // GET: Herds
        public ActionResult Index(string message, int? state)
        {
            if (message != null)
            {
                ViewBag.Message = message;
                ViewBag.State = state;
            }
            var herds = _dairyFarmService.GetHerdsIncludeCattle();
            return View(herds.ToList());
        }

        // GET: Herds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Herd herd = _dairyFarmService.GetHerdById((int)id);
            if (herd == null)
            {
                return HttpNotFound();
            }
            return View(herd);
        }

        // GET: Herds/Create
        public ActionResult Create()
        {
            ViewBag.IdCattleType = new SelectList(_dairyFarmService.GetCattleTypes(), "IdCattleType", "Label");

            return PartialView();
        }

        // POST: Herds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Herd herd, string id)
        {
            if (ModelState.IsValid)
            {
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Troupeau bien ajouté"
                };
                herd.AvailablePlaces = herd.MaxAnimals;
                if (_dairyFarmService.AddHerd(herd) == false)
                {
                    popup.State = 0;
                    popup.Message = "Erreur à la création";
                }
                if (id == "ajax")
                {
                    return RedirectToAction("Index","Cattle", new { message = popup.Message, state = popup.State });
                }
                return RedirectToAction("Index", new { message = popup.Message, state = popup.State });
            }
            if (id == "ajax")
            {
                return RedirectToAction("Index", "Cattle", new { message = "Erreur dans l'ajout du troupeau", state = 0 });
            }
            ViewBag.IdCattleType = new SelectList(_dairyFarmService.GetCattleTypes(), "IdCattleType", "Label", herd.IdCattleType);
            return RedirectToAction("Create", new { message = "Erreur dans l'ajout du troupeau", state = 0 });
        }

        // GET: Herds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Herd herd = _dairyFarmService.GetHerdById((int)id);
            if (herd == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCattleType = new SelectList(_dairyFarmService.GetCattleTypes(), "IdCattleType", "Label", herd.IdCattleType);
            return View(herd);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Herd herd)
        {
            if (ModelState.IsValid)
            {
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Troupeau bien édité"
                };
                var dbHerd = _dairyFarmService.GetHerdById(herd.IdHerd);
                dbHerd.Label = herd.Label;
                var available = herd.MaxAnimals - dbHerd.MaxAnimals + dbHerd.AvailablePlaces;
                dbHerd.AvailablePlaces = available;
                dbHerd.MaxAnimals = herd.MaxAnimals;
                if (_dairyFarmService.EditHerd(dbHerd) == false)
                {
                    popup.State = 0;
                    popup.Message = "Erreur dans l'édition";
                }
                return RedirectToAction("Index", new { message = popup.Message, state = popup.State });
            }
            ViewBag.IdCattleType = new SelectList(_dairyFarmService.GetCattleTypes(), "IdCattleType", "Label", herd.IdCattleType);
            return RedirectToAction("Create", new { message = "Erreur dans l'édition", state = 0 });
        }

        // GET: Herds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Herd herd = _dairyFarmService.GetHerdById((int)id);
            if (herd == null)
            {
                return HttpNotFound();
            }
            return View(herd);
        }

        // POST: Herds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Herd herd = _dairyFarmService.GetHerdById(id);
            herd.Active = true;
            var popup = new MessageInfo
            {
                State = 1,
                Message = "Troupeau bien supprimé"
            };
            if (_dairyFarmService.EditHerd(herd) == false)
            {
                popup.State = 0;
                popup.Message = "Erreur dans la suppression";
            }
            return RedirectToAction("Index", new { message = popup.Message, state = popup.State });
        }

    
    }
}
