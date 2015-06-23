using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Web.Models
{
    public class NewGestationModel
    {
        public int idCattleInGestation { get; set; }
        public int IdGestation { get; set; }
        public int IdCattle { get; set; }

        [Required(ErrorMessage = "Entrez une {0}")]
        [Display(Name = "Date du debut")]
        [DataType(DataType.Text)]
        public System.DateTime StartDateGestation { get; set; }

        [Display(Name = "Date de fin")]
        [DataType(DataType.Text)]
        public DateTime? EndDateGestation { get; set; }

        [Display(Name = "Date du foetus")]
        [DataType(DataType.Text)]
        public DateTime? DateCalve { get; set; }

        [Display(Name = "Sexe du foetus")]
        public string CalveSex { get; set; }

        [Display(Name = "Commentaire")]
        public string Comment { get; set; }

        [Display(Name = "Mort-né ?")]
        public Nullable<bool> DeathCalve { get; set; }
    }
}