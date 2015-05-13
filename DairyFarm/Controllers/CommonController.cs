using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DairyFarm.DAL;

namespace DairyFarm.Controllers
{
    public class CommonController : Controller
    {

        private readonly DairyFarmEntities _db = new DairyFarmEntities();
        // GET: API/Common
        [HttpPost]
        public ActionResult GetHerdsByCattleType(int? idCattleType)
        {
            if (idCattleType == null)
                return null;
            var herds = new List<Object>();
            foreach (var herd in _db.Herds.Where(h => h.IdCattleType == idCattleType).ToList())
            {
                herds.Add(new { Value = herd.IdHerd, Text = herd.Label });
            }
            return Json(herds);
            //, JsonRequestBehavior.AllowGet
        }

        [HttpPost]
        public ActionResult CreateHerd(int? idCattleType, int? maxAnimals, string label)
        {
            var herds = new List<Object>();
            if (idCattleType != null && maxAnimals != null)
            {
                var herd = new Herd
                {
                    IdCattleType = (int) idCattleType,
                    MaxAnimals = (int) maxAnimals,
                    Label = label
                };
                _db.Herds.Add(herd);
                _db.SaveChanges();
                //herds.Add(new { Value = herd.IdHerd, Text = herd.Label });
            }
            foreach (var herd in _db.Herds.Where(h => h.IdCattleType == idCattleType).ToList())
            {
                herds.Add(new { Value = herd.IdHerd, Text = herd.Label });
            }
            return Json(herds);
        }

        public ActionResult GetDisease(int? idCattleType)
        {
            
            var diseases = new List<Object>();
            foreach (var disease in _db.Diseases.ToList())
            {
                diseases.Add(new { Value = disease.IdDisease, Text = disease.Label });
            }
            return Json(diseases,JsonRequestBehavior.AllowGet);
            
        }


        public ActionResult GetMedicalTreatment(int? idCattleType)
        {
            
            var treatments = new List<Object>();
            foreach (var treatment in _db.MedicalTreatments.ToList())
            {
                treatments.Add(new { Value = treatment.IdTreatment, Text = treatment.Label });
            }
            return Json(treatments,JsonRequestBehavior.AllowGet);
        }
    }
}