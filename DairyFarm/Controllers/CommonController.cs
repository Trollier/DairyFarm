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

        private DairyFarmEntities db = new DairyFarmEntities();
        // GET: API/Common
        [HttpPost]
        public ActionResult GetHerdsByCattleType(int? idCattleType)
        {
            if (idCattleType == null)
                return null;
            var herds = new List<Object>();
            foreach (var herd in db.Herds.Where(h => h.IdCattleType == idCattleType).ToList())
            {
                herds.Add(new { Value = herd.IdHerd, Text = herd.Label });
            }
            return Json(herds);
            //, JsonRequestBehavior.AllowGet
        }
    }
}