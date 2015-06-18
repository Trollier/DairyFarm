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
        public ActionResult Index()
        {
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

            return View();
        }

        // POST: Herds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Herd herd)
        {
            if (ModelState.IsValid)
            {
                herd.AvailablePlaces = herd.MaxAnimals;
                _dairyFarmService.AddHerd(herd);
                return RedirectToAction("Index");
            }

            ViewBag.IdCattleType = new SelectList(_dairyFarmService.GetCattleTypes(), "IdCattleType", "Label", herd.IdCattleType);
            return View(herd);
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

        // POST: Herds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Herd herd)
        {
            if (ModelState.IsValid)
            {
                var dbHerd = _dairyFarmService.GetHerdById(herd.IdHerd);
                dbHerd.Label = herd.Label;
                var available = herd.MaxAnimals - dbHerd.MaxAnimals + dbHerd.AvailablePlaces;
                dbHerd.AvailablePlaces = available;
                dbHerd.MaxAnimals = herd.MaxAnimals;
                _dairyFarmService.EditHerd(dbHerd);
                return RedirectToAction("Index");
            }
            ViewBag.IdCattleType = new SelectList(_dairyFarmService.GetCattleTypes(), "IdCattleType", "Label", herd.IdCattleType);
            return View(herd);
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
            _dairyFarmService.EditHerd(herd);
            return RedirectToAction("Index");
        }

    
    }
}
