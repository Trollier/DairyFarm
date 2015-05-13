using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DairyFarm.DAL;

namespace DairyFarm.Models
{
    public class CattleDetailViewModel
    {
        public string CodeCattle { get; set; }
        public string Cattletype { get; set; }
        public string Herd { get; set; }
        public string State { get; set; }
        public int Age { get; set; }
        public Nullable<int> MalParent { get; set; }
        public Nullable<int> FemaleParent { get; set; }
        public string Sex { get; set; }
        public Gestation CurrentGestation { get; set; }
        public DiseasesHistory CurrentDisease { get; set; }

    }
}