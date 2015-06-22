using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(GestationMetaData))]
    public partial class Gestation
    {

    }
    public class GestationMetaData
    {
        [Required(ErrorMessage = "Entrez une {0}")]
        [Display(Name = "Date du debut")]
        //[DataType(DataType.Date)]
        public System.DateTime StartDateGestation { get; set; }

        [Display(Name = "Date de fin")]
        public Nullable<System.DateTime> EndDateGestation { get; set; }

        [Display(Name = "Date du foetus")]
        public Nullable<System.DateTime> DateCalve { get; set; }

        [Display(Name = "Sexe du foetus")]
        public string CalveSex { get; set; }

        [Display(Name = "Commentaire")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [Display(Name = "Mort-né ?")]
        public Nullable<bool> DeathCalve { get; set; }
    }
}
