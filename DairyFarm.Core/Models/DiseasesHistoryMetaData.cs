using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(DiseasesHistoryMetaData))]
    public partial class DiseasesHistory
    {
        public ICollection<int> IdMedicalTreatments { get; set; }


    }
    public class DiseasesHistoryMetaData
    {

        [Required(ErrorMessage = "Select une {0}")]
        [Display(Name = "Maladie")]
        public int IdDisease { get; set; }

        [Required(ErrorMessage = "Entrez une {0}")]
        [Display(Name = "Date du debut")]
        public System.DateTime StartDate { get; set; }

        [Display(Name = "Date de fin")]
        public Nullable<System.DateTime> EndDate { get; set; }

        [Display(Name = "visite du veterinaire ?")]
        public bool VeterinaryVisit { get; set; }
    }
}
