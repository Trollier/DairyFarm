using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(SeasonMetaData))]
    public partial class Season
    {

    }
    public class SeasonMetaData
    {
        [Required(ErrorMessage = "Entrez une {0}")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Nom de la Saison")]
        public string Label { get; set; }

        [Required(ErrorMessage = "Entrez une {0}")]
        [Display(Name = "Date du debut")]
        [DataType(DataType.Text)]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Entrez une {0}")]
        [Display(Name = "Date de fin")]
        [DataType(DataType.Text)]
        public System.DateTime EndDate { get; set; }
    }
}
