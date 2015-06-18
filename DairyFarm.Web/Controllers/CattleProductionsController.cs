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
using System.Web.Routing;
using DairyFarm.Core.DAL;
using DairyFarm.Service;
using DairyFarm.Web.Models;

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
        public ActionResult Index( string message, int? state)
        {
            var cattleProductions = _dairyFarmService.GetProductions();
               if (message != null)
            {
                ViewBag.Message = message;
                ViewBag.State = state;
            }
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
            var p = period == "matin" ? 6 : 18;
            ICollection<CattleProduction> cattleProductions = new List<CattleProduction>();
            var ListCattles = _dairyFarmService.GetCattlesMilk();
            var yesterdayProd = _dairyFarmService.GetYesterdayProd(DateTime.Now);
            foreach (var cattle in ListCattles)
            {
                var production = cattle.CattleProductions.FirstOrDefault(c => c.Period.Hour == p && c.Dateprod.Month == DateTime.Now.Month && c.Dateprod.Day == DateTime.Now.Day);
                if (production == null )
                {
                    cattleProductions.Add(new CattleProduction
                    {
                        IdCattle = cattle.IdCattle,
                        Cattle = cattle,
                        Quantity2 = null,
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
                        cattleprod.Quantity = prod.Quantity;
                    }
                }
            }
            ViewBag.message = " Le " + DateTime.Now.ToString("dddd dd MM yyyy");
            if (!cattleProductions.Any())
            {
               MessageInfo message = new MessageInfo();
                message.Message = "Les production ont déja été introduite";
                message.State = 0;
                return RedirectToAction("Index", new { message = message.Message, state = message.State });
            }
            return View(cattleProductions);
        }


        public ActionResult DifferentDateProd(DateTime dateProdDifferent,string period)
        {
            var p = period == "matin" ? 6 : 18;
            ICollection<CattleProduction> cattleProductions = new List<CattleProduction>();
            var ListCattles = _db.Cattles.Where(c => c.Herd.IdCattleType == 3);
            foreach (var cattle in ListCattles)
            {
                var production = cattle.CattleProductions.FirstOrDefault(c => c.Period.Hour == p && c.Dateprod.Month == dateProdDifferent.Month && c.Dateprod.Day == dateProdDifferent.Day);
                if (production == null || production.Quantity == 0)
                {
                    cattleProductions.Add(new CattleProduction
                    {
                        IdCattle = cattle.IdCattle,
                        Cattle = cattle,
                        Quantity2 = null,
                        Dateprod = dateProdDifferent,
                        Period = new DateTime(
                            dateProdDifferent.Year,
                            dateProdDifferent.Month,
                             dateProdDifferent.Day,
                            p,
                             0,
                            0
                            )
                    });
                }
            }

           
            ViewBag.message = " Le " + dateProdDifferent.ToString("dddd dd MM yyyy");
            if (!cattleProductions.Any())
            {
                MessageInfo message = new MessageInfo();
                message.Message = "Les production ont déja été introduite";
                message.State = 0;
                return RedirectToAction("Index", new { message = message.Message, state = message.State });
            }
            return View("SetProductions", cattleProductions);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetProductions(ICollection<CattleProduction> cattleProductions)
        {
            if (ModelState.IsValid)
            {
                MessageInfo popup = new MessageInfo
                {
                    Message = "Production bien ajouté",
                    State = 1
                };
                
                foreach (var production in cattleProductions)
                {
                    if (production.Quantity2 != null)
                    {
                        production.Quantity = (decimal) production.Quantity2;
                        if (_dairyFarmService.AddCattleProduction(production) == false)
                        {
                            popup.Message = "Erreur dans l'ajout";
                            popup.State = 0;
                        }
                    }
                    
                }
                
                return RedirectToAction("Index",new{ message = popup.Message, state = popup.State});
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
            return PartialView(cattleProduction);
        }

        // POST: CattleProductions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( CattleProduction cattleProduction)
        {
            MessageInfo popup = new MessageInfo
            {
                Message = "Production bien ajouté",
                State = 1
            };
            if (ModelState.IsValid)
            {

                if (_dairyFarmService.EditCattleProductions(cattleProduction) == false)
                {
                    popup.Message = "Erreur dans l'editon";
                    popup.State = 0;
                }
                return RedirectToAction("Index", new {message = popup.Message,state = popup.State});
            }
            popup.Message = "Erreur dans l'editon";
            popup.State = 0;
            return RedirectToAction("Index", new { message = popup.Message, state = popup.State });
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
