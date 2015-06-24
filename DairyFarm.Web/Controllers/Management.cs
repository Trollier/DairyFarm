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
        public ActionResult Main()
        {

            return View();
        }
    }
}
