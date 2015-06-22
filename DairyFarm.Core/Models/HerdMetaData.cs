using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(HerdMetaData))]
    public partial class Herd
    {

    }
    public class HerdMetaData
    {

        [Required(ErrorMessage = "Select un {0}")]
        [Display(Name = "Type bête")]
        public int IdCattleType { get; set; }

        [Display(Name = "Max Animaux")]
        public int MaxAnimals { get; set; }

        [Required(ErrorMessage = "Entrez une {0}")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Nom du troupeau")]
        public string Label { get; set; }

        [Display(Name = "Places disponibles")]
        public int AvailablePlaces { get; set; }
    }
}
