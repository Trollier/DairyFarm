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

        [Required(ErrorMessage = "Entrez une {0}")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Etiquette")]
        public string Label { get; set; }

        [Display(Name = "contagieux ?")]
        public bool Contagious { get; set; }
    }
}
