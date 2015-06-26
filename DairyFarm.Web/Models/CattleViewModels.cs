using System;
using System.Collections.Generic;
using DairyFarm.Core.DAL;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace DairyFarm.Web.Models
{
    public class CattleViewModel
    {

        public string IdCattle { get; set; }

        [Display(Name = "Code Bête")]
        public string CodeCattle { get; set; }

        [Display(Name = "Type de la bête")]
        public string Cattletype { get; set; }

        [Display(Name = "Troupeau")]
        public string Herd { get; set; }

        public bool CurrentGestation { get; set; }
        public bool CurrentDisease { get; set; }
        public int Age { get; set; }
    }
    public class CattleCreateViewModel
    {

        [Required(ErrorMessage = "Entrez un {0}")]
        [Display(Name = "Code Bête")]
        [RegularExpression(@"^[A-Z]{2}-[0-9]{2}-[0-9]{7}$", ErrorMessage = "Code invalide. ex: MA-12-1234567")]
        public string CodeCattle { get; set; }

        [Required(ErrorMessage = "Select un {0}")]
        [Display(Name = "Type de la bête")]
        public int IdCattletype { get; set; }

        [Required(ErrorMessage = "Select un {0}")]
        [Display(Name = "Troupeau")]
        public int IdHerd { get; set; }

        [Required(ErrorMessage = "Entrez une {0}")]
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Text)]
        public System.DateTime DateBirth { get; set; }

        [Display(Name = "Parent Male")]
        public int? MalParent { get; set; }

        [Display(Name = "Parent femelle")]
        public int? FemaleParent { get; set; }

        [Required(ErrorMessage = "Entrez une {0}")]
        [Display(Name = "Sexe")]
        public string Sex { get; set; }

        // Start Disease
        [Display(Name = "Est malade ?")]
        public bool HealthState { get; set; }
        public DiseasesHistory CurrentDisease { get; set; }

        // End Disease 

        // Start Gestation 
        [Display(Name = "En gestation ?")]
        public bool IsGestation { get; set; }
        public Gestation CurrentGestation { get; set; }
        // End Gestation 

    }
    public class CattleDetailViewModel
    {
        public CattleDetailViewModel()
        {
            CurrentDiseases = new List<DiseasesHistory>();
        }
        public int IdCattle { get; set; }

        [Display(Name = "Code Bête")]
        public string CodeCattle { get; set; }

        [Display(Name = "Type de la bête")]
        public string Cattletype { get; set; }

        [Display(Name = "Troupeau")]
        public string LabelHerd { get; set; }

        [Display(Name = "Age-année")]
        public int AgeYear { get; set; }

        [Display(Name = "Age-mois")]
        public int AgeMonth { get; set; }

        [Display(Name = "Parent male")]
        public int? MalParent { get; set; }

        [Display(Name = "Parent femelle")]
        public int? FemaleParent { get; set; }

        [Display(Name = "Sexe")]
        public string Sex { get; set; }

        public Boolean canGestation { get; set; }
        public List<DiseasesHistory> CurrentDiseases { get; set; }
        public Gestation CurrentGestation { get; set; }
    }

    public class CattleEditViewModel
    {

        public int idCattle { get; set; }

        [Display(Name = "Code Bête")]
        public string CodeCattle { get; set; }
        public int AgeYear { get; set; }
        public int AgeMonth { get; set; }
        [Display(Name = "Parent Mal")]
        public int? MalParent { get; set; }
        [Display(Name = "Parent femelle")]
        public int? FemaleParent { get; set; }

    }

    public class CattleDiseaseList
    {
        public CattleDiseaseList()
        {
            inQuarantine=new List<Cattle>();
            sick = new List<Cattle>();
        }
        public List<Cattle> inQuarantine { get; set; }
        public List<Cattle> sick { get; set; }

    }

}