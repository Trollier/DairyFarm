using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using DairyFarm.Core.DAL;
using DairyFarm.Service;
using DairyFarm.Web.Models;

namespace DairyFarm.Web.Controllers
{
    //[Authorize]
    public class CattleController : Controller
    {
        //private readonly DairyFarmEntities _db = new DairyFarmEntities();
        private readonly IDairyFarmService _dairyFarmService;

        public CattleController(IDairyFarmService dairyFarmService)
        {
            _dairyFarmService = dairyFarmService;
        }

        // GET: Cattle
        public ActionResult Index()
        {
            var cattleViewModels = new List<CattleViewModel>();
            ////
            /// ATTENTION A IMPLEMENTER A FOND
            /// 
            var listGrouping = _dairyFarmService.IndexCattle();

            return View(listGrouping);
        }

        // GET: Cattle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } 
            Cattle cattle = _dairyFarmService.GetCattleById(id);
           
            if (cattle == null)
            {
                return HttpNotFound();
            }
            var currentGestation = cattle.Gestations.FirstOrDefault(g => g.EndDateGestation == null);
            var currentDisease = cattle.DiseasesHistories.Where(d => d.EndDate == null).ToList();
            var cattleDetailViewModel = new CattleDetailViewModel
            {
                idCattle = cattle.IdCattle,
                CodeCattle = cattle.CodeCattle,
                Cattletype = cattle.Herd.CattleType.Label,
                LabelHerd = cattle.Herd.Label,
                AgeYear = DateTime.Now.Year - cattle.DateBirth.Year,
                AgeMonth = DateTime.Now.Month - cattle.DateBirth.Month,
                MalParent = cattle.MalParent,
                FemaleParent = cattle.FemaleParent,
                Sex = cattle.Herd.CattleType.Sex,
                CurrentGestation = currentGestation,
            };
            foreach (DiseasesHistory disease in currentDisease)
            {
                if (disease != null)
                {
                    cattleDetailViewModel.currentDiseases.Add(disease);
                }
            }

            return View(cattleDetailViewModel);
        }

        // GET: Cattle/Create
        public ActionResult NewDisease()
        {
            // (liste a retourner, value, text)
            ViewBag.IdMedicalTreatments = new SelectList(_dairyFarmService.GetMedicalTreatments(), "IdTreatment", "Label");
            ViewBag.IdDisease = new SelectList(_dairyFarmService.GetDiseases(), "IdDisease", "Label");
            return PartialView("_DiseasesHistory");
        }

        public ActionResult NewGestation()
        {
            return PartialView("_Gestation");
        }
        // GET: Cattle/Create
        public ActionResult Create()
        {
            ViewBag.IdCattletype = new SelectList(_dairyFarmService.GetCattleTypes(), "IdCattletype", "Label");
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
                    DateBirth = cattleCreateViewModel.DateBirth,
                };
                //_db.Cattles.Add(cattle);
                //_db.SaveChanges();
                _dairyFarmService.AddCattle(cattle);

                if (cattleCreateViewModel.CurrentDisease != null)
                {
                    cattleCreateViewModel.CurrentDisease.IdCattle = cattle.IdCattle;

                    foreach (var idTreatment in cattleCreateViewModel.IdMedicalTreatments)
                    {
                        var medic = _dairyFarmService.GetMedicalTreatmentById(idTreatment);
                        cattleCreateViewModel.CurrentDisease.MedicalTreatments.Add(medic);
                    }

                    if (_dairyFarmService.AddDiseasesHistory(cattleCreateViewModel.CurrentDisease))
                    {

                    }
                    else
                    {
                        
                    };

                }


                if (cattleCreateViewModel.CurrentGestation != null)
                {
                    cattleCreateViewModel.CurrentGestation.IdCattle = cattle.IdCattle;
                    _dairyFarmService.AddDGestation(cattleCreateViewModel.CurrentGestation);
                }
                return RedirectToAction("Index");
            }

            ViewBag.IdCattletype = new SelectList(_dairyFarmService.GetCattleTypes(), "IdCattletype", "Label", cattleCreateViewModel.IdCattletype);
            ViewBag.IdHerd = new SelectList(_dairyFarmService.GetHerds(), "IdHerd", "Label", cattleCreateViewModel.IdHerd);
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
