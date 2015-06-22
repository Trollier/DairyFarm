using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(DietMetaData))]
    public partial class Diet
    {
        public ICollection<int> IdCattleTypes { get; set; }
        public ICollection<int> IdFoods { get; set; }
    }
    public class DietMetaData
    {
        [Required(ErrorMessage = "Select le {0}")]
        [Display(Name = "Saison")]
        public int IdSeason { get; set; }

        [Required(ErrorMessage = "Entrez le {0}")]
        [Display(Name = "Nom du régime")]
        public string Label { get; set; }

        [Required(ErrorMessage = "Entrez le {0}")]
        [Display(Name = "Type bête (s)")]
        public ICollection<int> IdCattleTypes { get; set; }

        [Required(ErrorMessage = "Entrez {0}")]
        [Display(Name = "Nourriture(s)")]
        public ICollection<int> IdFoods { get; set; }
    }
}
