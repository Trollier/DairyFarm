using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DairyFarm.Core.DAL;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Web.Models
{
    public class CattleViewModel
    {
       
        public int idCattle { get; set; }

        [Display(Name = "Code Bête")]
        public string CodeCattle { get; set; }

        [Display(Name = "Type de la bête")]
        public string Cattletype { get; set; }

        [Display(Name = "Troupeau")]
        public string Herd { get; set; }

        public string IdCattle { get; set; }
        public bool CurrentGestation { get; set; }
        public bool CurrentDisease { get; set; }
        public int Age { get; set; }
    }
    public class CattleCreateViewModel
    {

        [Required(ErrorMessage = "Entrez un {0}")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Code Bête")]
        [RegularExpression(@"^[A-Z]{2}-[0-9]{2}-[0-9]{3}$", ErrorMessage = "Code invalide.")]
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
        public Nullable<int> MalParent { get; set; }

        [Display(Name = "Parent femelle")]
        public Nullable<int> FemaleParent { get; set; }

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
            currentDiseases = new List<DiseasesHistory>();
        }
        public int idCattle { get; set; }

        [Display(Name = "Code Bête")]
        public string CodeCattle { get; set; }

        [Display(Name = "Type de la bête")]
        public string Cattletype { get; set; }

        [Display(Name = "Troupeau")]
        public string LabelHerd { get; set; }

        public int AgeYear { get; set; }
        public int AgeMonth { get; set; }

        [Display(Name = "Parent Mal")]
        public Nullable<int> MalParent { get; set; }

        [Display(Name = "Parent femelle")]
        public Nullable<int> FemaleParent { get; set; }

        [Display(Name = "Sexe")]
        public string Sex { get; set; }
        public List<DiseasesHistory> currentDiseases { get; set; }
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
        public Nullable<int> MalParent { get; set; }
        [Display(Name = "Parent femelle")]
        public Nullable<int> FemaleParent { get; set; }

    }
}