using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DairyFarm.Core.DAL;
using DairyFarm.Service;

namespace DairyFarm.Web.Controllers
{
    public class CattleProductionsController : Controller
    {
        private readonly DairyFarmEntities _db = new DairyFarmEntities();
        private readonly IDairyFarmService _dairyFarmService;

        public CattleProductionsController(IDairyFarmService dairyFarmService)
        {
            _dairyFarmService = dairyFarmService;
        }
        // GET: CattleProductions
        public ActionResult Index()
        {
            var cattleProductions = _dairyFarmService.GetCattleProductions();
            return View(cattleProductions.ToList());
        }

        // GET: CattleProductions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CattleProduction cattleProduction = _dairyFarmService.GetCattleProductionById(id);
            if (cattleProduction == null)
            {
                return HttpNotFound();
            }
            return View(cattleProduction);
        }


        public ActionResult SetProductions(string period)
        {
            var p = period == "matin" ? 6 : 9;
            ICollection<CattleProduction> cattleProductions = new List<CattleProduction>();
            var ListCattles = _db.Cattles.Where(c => c.Herd.IdCattleType == 3);
            var yesterdayProd = _dairyFarmService.GetYesterdayProd(DateTime.Now);
            foreach (var cattle in ListCattles)
            {
                var production = cattle.CattleProductions.FirstOrDefault(c => c.Period.Hour == p);
                if (production == null || production.Quantity == 0)
                {
                    cattleProductions.Add(new CattleProduction
                    {
                        IdCattle = cattle.IdCattle,
                        Cattle = cattle,
                        Dateprod = DateTime.Now,
                        Period = new DateTime(
                            DateTime.Now.Year, 
                            DateTime.Now.Month,
                             DateTime.Now.Day,
                            p,
                             0,
                            0
                            )
                    });
                }
            }

            foreach (var prod in yesterdayProd)
            {
                foreach (var cattleprod in cattleProductions)
                {
                    if (prod.IdCattle == cattleprod.IdCattle)
                    {
                        prod.Quantity = cattleprod.Quantity;
                    }
                }
            }
            ViewBag.message = " Le " + DateTime.Now.ToString("dddd dd MM yyyy");
            return View(cattleProductions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetProductions(ICollection<CattleProduction> cattleProductions)
        {
            if (ModelState.IsValid)
            {
                foreach (var production in cattleProductions)
                {
                    _dairyFarmService.AddCattleProduction(production);
                }

                return RedirectToAction("Index");
            }

            ViewBag.message = " Le " + DateTime.Now.ToString("dddd dd MM yyyy");
            return View(cattleProductions);
        }


        // GET: CattleProductions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CattleProduction cattleProduction = _dairyFarmService.GetCattleProductionById(id);
            if (cattleProduction == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCattle = new SelectList(_dairyFarmService.GetCattles(), "IdCattle", "CodeCattle", cattleProduction.IdCattle);
            return View(cattleProduction);
        }

        // POST: CattleProductions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduction,IdCattle,Dateprod,hourprod,quantity")] CattleProduction cattleProduction)
        {
            if (ModelState.IsValid)
            {

                _dairyFarmService.AddCattleProduction(cattleProduction);
                return RedirectToAction("Index");
            }
            ViewBag.IdCattle = new SelectList(_dairyFarmService.GetCattles(), "IdCattle", "CodeCattle", cattleProduction.IdCattle);
            return View(cattleProduction);
        }

        // GET: CattleProductions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CattleProduction cattleProduction = _dairyFarmService.GetCattleProductionById(id);
            if (cattleProduction == null)
            {
                return HttpNotFound();
            }
            return View(cattleProduction);
        }

        // POST: CattleProductions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CattleProduction cattleProduction = _dairyFarmService.GetCattleProductionById(id);
            _db.CattleProductions.Remove(cattleProduction);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
