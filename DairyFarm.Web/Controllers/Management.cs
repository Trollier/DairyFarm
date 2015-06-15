using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using DairyFarm.Web.Models;

namespace DairyFarm.Web.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Manage
        public ActionResult Main(string id, string message, int? state)
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

            
            ViewBag.ListItem = new SelectList(listItems, "Text", "Value");
            if (state != null)
            {
                var popup = new MessageInfo { Id = id, Message = message, State = state };
                return View(popup);

            }
            return View();
        }
    }
}
