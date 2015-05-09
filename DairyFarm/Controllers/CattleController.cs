using DairyFarm.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using DairyFarm.Models;

namespace DairyFarm.Controllers
{
    public class CattleController : Controller
    {
        private DairyFarmEntities db = new DairyFarmEntities();

        // GET: Cattle
        public ActionResult Index()
        {
            var cattles = db.Cattles.Include(c => c.Herd).Include(c => c.HealthState);
            var cattleViewModels = new List<CattleViewModel>();
            foreach (var cattle in cattles)
            {
                var cattleViewModel = new CattleViewModel
                {
                    CodeCattle = cattle.CodeCattle,
                    Cattletype = cattle.Herd.CattleType.Label,
                    Herd = cattle.Herd.Label,
                    State = cattle.HealthState.Label,


                    CurrentGestation = cattle.Gestations.FirstOrDefault(g => g.EndDate == null)==null? true:false,
                    CurrentDisease = cattle.DiseasesHistories.FirstOrDefault(d => d.EndDate == null).Disease.Label,
                };
                cattleViewModels.Add(cattleViewModel);
            }
            return View(cattleViewModels);
        }

        // GET: Cattle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cattle cattle = db.Cattles.Find(id);
            if (cattle == null)
            {
                return HttpNotFound();
            }
            var currentGestation = cattle.Gestations.FirstOrDefault(g => g.EndDate == null);
            var currentDisease = cattle.DiseasesHistories.FirstOrDefault(d => d.EndDate == null);
            var cattleDetailViewModel = new CattleDetailViewModel
            {
                CodeCattle = cattle.CodeCattle,
                Cattletype = cattle.Herd.CattleType.Label,
                Herd = cattle.Herd.Label,
                State = cattle.HealthState.Label,
                Sex = cattle.Sex,
                CurrentGestation = currentGestation,
                CurrentDisease = currentDisease
            };
            return View(cattleDetailViewModel);
        }

        // GET: Cattle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cattle/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cattle/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cattle/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cattle/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cattle/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
