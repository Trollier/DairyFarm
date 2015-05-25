using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DairyFarm.DAL;

namespace DairyFarm.Models
{
    public class CattleCreateViewModel
    {
        public string CodeCattle { get; set; }
        public int IdCattletype { get; set; }
        public int IdHerd { get; set; }
        public System.DateTime DateBirth { get; set; }
        public Nullable<int> MalParent { get; set; }
        public Nullable<int> FemaleParent { get; set; }
        public string Sex { get; set; }

        // Start Disease
        public bool HealthState { get; set; }
        public DiseasesHistory CurrentDisease { get; set; }
        public ICollection<int> IdMedicalTreatments { get; set; }

        // End Disease 

        // Start Gestation 
        public bool IsGestation { get; set; }
        public Gestation CurrentGestation { get; set; }
        // End Gestation 

    }
}