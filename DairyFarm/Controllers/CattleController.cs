﻿using DairyFarm.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using DairyFarm.Models;
using DairyFarm.Services;

namespace DairyFarm.Controllers
{
    public class CattleController : Controller
    {
        private readonly DairyFarmEntities _db = new DairyFarmEntities();
        
        // GET: Cattle
        public ActionResult Index()
        {
            var cattles = _db.Cattles.Include(c => c.Herd);
            var cattleViewModels = new List<CattleViewModel>();

            foreach (var cattle in cattles)
            {
                var cattleViewModel = new CattleViewModel
                {
                    CodeCattle = cattle.CodeCattle,
                    Cattletype = cattle.Herd.CattleType.Label,
                    Herd = cattle.Herd.Label,
                    Age =  DateTime.Now.Year- cattle.DateBirth.Year ,

                    CurrentGestation = cattle.Gestations.FirstOrDefault(g => g.EndDateGestation == null)!=null,
                    CurrentDisease = cattle.DiseasesHistories.FirstOrDefault(g => g.EndDate == null) != null
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
            Cattle cattle = _db.Cattles.Find(id);
            if (cattle == null)
            {
                return HttpNotFound();
            }
            var currentGestation = cattle.Gestations.FirstOrDefault(g => g.EndDateGestation == null);
            var currentDisease = cattle.DiseasesHistories.FirstOrDefault(d => d.EndDate == null);
            var cattleDetailViewModel = new CattleDetailViewModel
            {
                CodeCattle = cattle.CodeCattle,
                Cattletype = cattle.Herd.CattleType.Label,
                Herd = cattle.Herd.Label,
                Sex = cattle.Sex,
                CurrentGestation = currentGestation,
                CurrentDisease = currentDisease
            };
            return View(cattleDetailViewModel);
        }

        // GET: Cattle/Create
        public ActionResult NewDisease()
        {
            // (liste a retourner, value, text)
            ViewBag.IdMedicalTreatments = new SelectList(_db.MedicalTreatments, "IdTreatment", "Label");
            ViewBag.IdDisease = new SelectList(_db.Diseases, "IdDisease", "Label");
            return PartialView("_DiseasesHistory");
        }

        public ActionResult NewGestation()
        {
            return PartialView("_Gestation");
        }
        // GET: Cattle/Create
        public ActionResult Create()
        {
            ViewBag.IdCattletype = new SelectList(_db.CattleTypes, "IdCattletype", "Label");
            ViewBag.IdHerd = new SelectList(new List<Herd>(), "IdHerd", "Label");
         
            return View();
        }

        // POST: Cattle/Create
        [HttpPost]
        public ActionResult Create(CattleCreateViewModel cattleCreateViewModel)
        {

            if (ModelState.IsValid)
            {
                var cattle = new Cattle
                {
                    CodeCattle = cattleCreateViewModel.CodeCattle,
                    IdHerd = cattleCreateViewModel.IdHerd,
                    Sex = cattleCreateViewModel.Sex,
                    DateBirth = cattleCreateViewModel.DateBirth,
                };
                _db.Cattles.Add(cattle);
                _db.SaveChanges();

                if (cattleCreateViewModel.CurrentDisease != null)
                {
                    cattleCreateViewModel.CurrentDisease.IdCattle = cattle.IdCattle;

                    foreach (var idTreatment in cattleCreateViewModel.IdMedicalTreatments)
                    {
                        var medic = _db.MedicalTreatments.Find(idTreatment);
                        cattleCreateViewModel.CurrentDisease.MedicalTreatments.Add(medic);
                    }
                    _db.DiseasesHistories.Add(cattleCreateViewModel.CurrentDisease);
                    _db.SaveChanges();

                }
                

                if (cattleCreateViewModel.CurrentGestation!=null)
                {
                    cattleCreateViewModel.CurrentGestation.IdCattle = cattle.IdCattle;
                    _db.Gestations.Add(cattleCreateViewModel.CurrentGestation);
                    _db.SaveChanges();

                }
                return RedirectToAction("Index");
            }

            ViewBag.IdCattletype = new SelectList(_db.CattleTypes, "IdCattletype", "Label", cattleCreateViewModel.IdCattletype);
            ViewBag.IdHerd = new SelectList(_db.Herds, "IdHerd", "Label", cattleCreateViewModel.IdHerd);
            return View(cattleCreateViewModel);
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
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
