using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;using DairyFarm.DAL;

namespace DairyFarm.Models
{
    public class CattleViewModel
    {
        public string CodeCattle { get; set; }
        public string Cattletype { get; set; }
        public string Herd { get; set; }
        public string IdCattle { get; set; }
        public string State { get; set; }
        public bool CurrentGestation { get; set; }
        public string CurrentDisease { get; set; }
        public int Age { get; set; }
    }
}