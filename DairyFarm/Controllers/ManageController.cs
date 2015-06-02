using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DairyFarm.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            var listItems = new List<object>
            {
                new{ Text ="Selectionnez",Value=""},
               new{ Text ="Nourriture",Value="Foods"},
               new{ Text="Traitement",Value="MedicalTreatments"},
               new{ Text="Maladie",Value="Diseases"},
               new{ Text="Saison",Value="Seasons"},
               new{ Text="Type de bétail",Value="CattleTypes"}
            };
            ViewBag.ListItem = new SelectList(listItems,"Text","Value");
            return View();
        }

    }
}
