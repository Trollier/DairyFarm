using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DairyFarm.DAL;

namespace DairyFarm.Models
{
    public class CattleDetailViewModel
    {
        public CattleDetailViewModel()
        {
                currentDiseases=new List<DiseasesHistory>();
        }
        public int idCattle { get; set; }
        public string CodeCattle { get; set; }
        public string Cattletype { get; set; }
        public string LabelHerd { get; set; }
        public int AgeYear { get; set; }
        public int AgeMonth { get; set; }
        public Nullable<int> MalParent { get; set; }
        public Nullable<int> FemaleParent { get; set; }
        public string Sex { get; set; }
        public List<DiseasesHistory> currentDiseases { get; set; }
        public Gestation CurrentGestation { get; set; }
    }
}