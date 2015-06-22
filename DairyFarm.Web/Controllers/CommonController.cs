﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DairyFarm.Core.DAL;
using DairyFarm.Service;

namespace DairyFarm.Web.Controllers
{
    public class CommonController : Controller
    {

        private readonly DairyFarmEntities _db = new DairyFarmEntities();
        // GET: API/Common
        [HttpPost]
        public ActionResult GetHerdsByCattleType(int? idCattleType, int age)
        {
            if (idCattleType == null)return null;
            var herds = new List<Object>();
            foreach (var herd in _db.Herds.Where(h => h.IdCattleType == idCattleType && h.Active==true).ToList())
            {
                herds.Add(new { Value = herd.IdHerd, Text = herd.Label });
            }
            return Json(herds);
        }
        public ActionResult GetType(string sex,DateTime date)
        {
            if (sex == null) return null;
            var rank = Util.GetRank(date);
            if (rank != 100) { 
            var types = new List<Object>();
            var rank1 = rank == 4 ? 3 : rank;
            foreach (var type in _db.CattleTypes.Where(h => h.Sex == sex && h.Rank >= rank1 && h.Rank <= rank).ToList())
            {
                types.Add(new { Value = type.IdCattleType, Text = type.Label });
            }
            return Json(types);
            }
            return null;
        }

        [HttpPost]
        public ActionResult CreateHerd(int? idCattleType, int? maxAnimals, string label)
        {
            var herds = new List<Object>();
            if (idCattleType != null && maxAnimals != null)
            {
                var herd = new Herd
                {
                    IdCattleType = (int)idCattleType,
                    MaxAnimals = (int)maxAnimals,
                    Label = label,
                    AvailablePlaces = (int)maxAnimals
                };
                _db.Herds.Add(herd);
                _db.SaveChanges();
            }
            foreach (var herd in _db.Herds.Where(h => h.IdCattleType == idCattleType).ToList())
            {
                herds.Add(new { Value = herd.IdHerd, Text = herd.Label });
            }
            return Json(herds);
        }

        public ActionResult GetDisease(string proposition)
        {

            var diseases = new List<Object>();
            foreach (var disease in _db.Diseases.Where(d => d.Label.Contains(proposition)).Take(15))
            {
                diseases.Add(new { Id = disease.IdDisease, Label = disease.Label });
            }
            return Json(diseases, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult GetDisease()
        {

            var diseases = new List<Object>();
            foreach (var disease in _db.Diseases.Take(15))
            {
                diseases.Add(new { Id = disease.IdDisease, Label = disease.Label });
            }
            return Json(diseases, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetMedicalTreatment(string proposition)
        {

            var treatments = new List<Object>();
            foreach (var treatment in _db.MedicalTreatments.ToList())
            {
                if (treatment.Label.Contains(proposition))
                {
                    treatments.Add(new { Id = treatment.IdTreatment, Label = treatment.Label });
                }
            }
            return Json(treatments, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gestation gestation)
        {

            if (ModelState.IsValid)
            {
                gestation.DateCalve = gestation.StartDateGestation.AddMonths(9);
                _db.Gestations.Add(gestation);
                _db.SaveChanges();
                return RedirectToAction("Details", "Cattle", new { id = gestation.IdCattle });
            }
            return RedirectToAction("Details", "Cattle", new { id = gestation.IdCattle });
        }

        public ActionResult LoadFoods()
        {
            var foods = new List<Object>();
            foreach (var food in _db.Foods.ToList())
            {

                foods.Add(new { Value = food.IdFood, Text = food.Label });
              
            }
            return Json(foods, JsonRequestBehavior.AllowGet);
        }

    }
}