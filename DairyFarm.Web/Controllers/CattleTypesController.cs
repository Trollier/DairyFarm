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
    [Authorize]
    public class CattleTypesController : Controller
    {
        //private DairyFarmEntities db = new DairyFarmEntities();
        private readonly IDairyFarmService _dairyFarmService;

        public CattleTypesController(IDairyFarmService dairyFarmService)
        {
            _dairyFarmService = dairyFarmService;
        }
        // GET: CattleTypes
        public ActionResult Index(string message, int? state)
        {
            if (message != null)
            {
                ViewBag.Message = message;
                ViewBag.State = state;
            }
            return View(_dairyFarmService.GetCattleTypes());
        }

        // GET: CattleTypes/Details/5


        // GET: CattleTypes/Create
        public ActionResult Create()
        {
            var listSelect = new List<object>
            {
                new {Text = "Mâle", Value = "M"},
                new {Text = "Femelle", Value = "F"}
            };
            ViewBag.List = new SelectList(listSelect, "Text", "Value");
            return View();
        }

        // POST: CattleTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CattleType cattleType)
        {
            if (ModelState.IsValid)
            {

                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Type bien ajouté"
                };
                if (_dairyFarmService.AddCattleType(cattleType) == false)
                {
                    return RedirectToAction("Index", "CattleTypes", new { message = "Erreur dans l'ajout", state = 0 });
                }
                return RedirectToAction("Index", "CattleTypes", new { message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "CattleTypes", new { message = "Erreur dans l'ajout", state = 0 });
        }

        // GET: CattleTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CattleType cattleType = _dairyFarmService.GetCattleTypeById(id);
            if (cattleType == null)
            {
                return HttpNotFound();
            }
            var listSelect = new List<object>
            {
                new {Text = "Mâle", Value = "M"},
                new {Text = "Femelle", Value = "F"}
            };
            ViewBag.List = new SelectList(listSelect, "Text", "Value");
            return View(cattleType);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CattleType cattleType)
        {
            if (ModelState.IsValid)
            {
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Type bien édité"
                };
                if (_dairyFarmService.EditCattleType(cattleType) == false)
                {
                    return RedirectToAction("Index", "CattleTypes", new { message = "Erreur dans l'édition", state = 0 });
                }
                return RedirectToAction("Index", "CattleTypes", new { id = popup.Id, message = popup.Message, state = popup.State });
            }
            return RedirectToAction("Index", "CattleTypes", new { message = "Erreur dans l'édition", state = 0 });
        }

        // GET: CattleTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CattleType cattleType = _dairyFarmService.GetCattleTypeById(id);
            if (cattleType == null)
            {
                return HttpNotFound();
            }
            return View(cattleType);
        }

    }
}
