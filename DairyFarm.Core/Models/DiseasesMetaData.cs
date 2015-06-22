using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(DiseasesMetaData))]
    public partial class Disease
    {

    }
    public class DiseasesMetaData
    {

        [Required(ErrorMessage = "Entrez le {0}")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Nom de la maladie")]
        public string Label { get; set; }

        [Display(Name = "Contagieux ?")]
        public bool Contagious { get; set; }
    }
}
