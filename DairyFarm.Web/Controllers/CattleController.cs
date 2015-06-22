using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Runtime.InteropServices;
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
        public ActionResult Index(string message,int? state)
        {
            var listGrouping = _dairyFarmService.IndexCattle();
            if (message != null)
            {
                ViewBag.Message = message;
                ViewBag.State = state;
            }
         
            return View(listGrouping);
        }

        // GET: Cattle/Details/5
        public ActionResult Details(int? id, string message, int? state)
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
            if (message != null)
            {
                ViewBag.Message = message;
                ViewBag.State = state;
            }
            return View(cattleDetailViewModel);
        }


        public ActionResult NewDisease()
        {
            // (liste a retourner, value, text)
            ViewBag.IdMedicalTreatments = new SelectList(_dairyFarmService.GetMedicalTreatments(), "IdTreatment", "Label");
            ViewBag.IdDisease = new SelectList(_dairyFarmService.GetDiseases(), "IdDisease", "Label");
            return PartialView("_DiseasesHistory");
        }

        public ActionResult NewGestation()
        {
            var CalveSex = new List<string> { "F", "M" };
            ViewBag.CalveSex = new SelectList(CalveSex);
            return PartialView("_Gestation");
        }

        public ActionResult CattleInQuarantine()
        {
            var cattleInquarantin = _dairyFarmService.GetCattleInQuarantine();
            return View(cattleInquarantin);
        }
        public ActionResult ChangeHerd(int idHerd)
        {
            var cattles = _dairyFarmService.GetCattlesByHerd(idHerd);
            var herds = _dairyFarmService.GetHerdListById(idHerd);
            ViewBag.Cattles = new SelectList(cattles, "IdCattle", "CodeCattle");
            ViewBag.Herds = new SelectList(herds, "IdHerd", "Label");
            return View();
        }
        [HttpPost]
        public ActionResult ChangeHerd(ChangeHerdViewModel changeHerd)
        {
            if (ModelState.IsValid) {
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "troupeau bien changé"
                };
               
            foreach (var cattle in changeHerd.IdChangeCattle)
            {
                 var herdDecrement =_dairyFarmService.GetCattleById(cattle);
                 _dairyFarmService.DecrementHerd(changeHerd.IdChangeHerd);
                 _dairyFarmService.IncrementHerd(herdDecrement.IdHerd);
               var cattleToEdit = _dairyFarmService.GetCattleById(cattle);
                cattleToEdit.IdHerd = changeHerd.IdChangeHerd;
                bool edited = _dairyFarmService.EditCattle(cattleToEdit);
            }
            return RedirectToAction("Index", new { message = popup.Message, state = popup.State });

            }
            return RedirectToAction("Create", new { message = "Erreur dans l'ajout", state = 0 });
        }
        // GET: Cattle/Create
        public ActionResult Create(string message, int? state)
        {
            ViewBag.IdCattletype = new SelectList(_dairyFarmService.GetCattleTypes(), "IdCattletype", "Label");
            ViewBag.IdHerd = new SelectList(new List<Herd>(), "IdHerd", "Label");
            if (message != null)
            {
                ViewBag.Message = message;
                ViewBag.State = state;
            }
            var Sex = new List<string> { "F", "M" };
            ViewBag.Sex = new SelectList(Sex);
            return View();
        }

        // POST: Cattle/Create
        [HttpPost]
        public ActionResult Create(CattleCreateViewModel cattleCreateViewModel)
        {

            if (ModelState.IsValid )
            {
                var popup = new MessageInfo
                {
                    State = 1,
                    Message = "Bête bien ajouté"
                };
                var cattle = new Cattle
                {
                    CodeCattle = cattleCreateViewModel.CodeCattle,
                    IdHerd = cattleCreateViewModel.IdHerd,
                    DateBirth = cattleCreateViewModel.DateBirth,
                };
                if(cattleCreateViewModel.CurrentDisease!=null){
                    }
                if (_dairyFarmService.AddCattle(cattle) == false)
                {
                    popup.State = 0;
                }


                if (cattleCreateViewModel.CurrentDisease != null)
                {
                    cattleCreateViewModel.CurrentDisease.IdCattle = cattle.IdCattle;

                    foreach (var idTreatment in cattleCreateViewModel.CurrentDisease.IdMedicalTreatments)
                    {
                        var medic = _dairyFarmService.GetMedicalTreatmentById(idTreatment);
                        cattleCreateViewModel.CurrentDisease.MedicalTreatments.Add(medic);
                    }

                    if (_dairyFarmService.AddDiseasesHistory(cattleCreateViewModel.CurrentDisease) == false)
                    {
                        popup.State = 0;
                    }

                }


                if (cattleCreateViewModel.CurrentGestation != null)
                {
                    cattleCreateViewModel.CurrentGestation.IdCattle = cattle.IdCattle;
                    if (_dairyFarmService.AddGestation(cattleCreateViewModel.CurrentGestation) == false)
                    {
                        popup.State = 0;
                    }
                }

                if (popup.State == 1)
                {
                    return RedirectToAction("Index", new {message = popup.Message, state = popup.State});

                }
                else
                {
                    return RedirectToAction("Create", new { message = "Erreur dans l'ajout", state = popup.State });
                    
                }

            }

            ViewBag.IdCattletype = new SelectList(_dairyFarmService.GetCattleTypes(), "IdCattletype", "Label", cattleCreateViewModel.IdCattletype);
            ViewBag.IdHerd = new SelectList(_dairyFarmService.GetHerds(), "IdHerd", "Label", cattleCreateViewModel.IdHerd);
            return RedirectToAction("Create", new { message = "Erreur dans l'ajout", state = 0 });
        }

        // GET: Cattle/Edit/5
        public ActionResult Edit(int id)
        {
            var cattle = _dairyFarmService.GetCattleById(id);

            return View(cattle);
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
            return PartialView(_dairyFarmService.GetCattleById(id));
        }

        // POST: Cattle/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (_dairyFarmService.DeleteCattle(id))
            {
                return RedirectToAction("Index", new { message = "Bien supprimer", state = 1 });
            }
            else
            {
                return RedirectToAction("Index", new { message = "Erreur dans la suppession", state = 0 });
                
            }

        }
    }
}
