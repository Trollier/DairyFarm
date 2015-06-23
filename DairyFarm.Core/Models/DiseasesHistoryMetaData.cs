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

        [Required(ErrorMessage = "Entrez la {0}")]
        [Display(Name = "Date du debut")]
        [DataType(DataType.Text)]
        public System.DateTime StartDate { get; set; }

        [Display(Name = "Date de fin")]
        [DataType(DataType.Text)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Visite du veterinaire ?")]
        public bool VeterinaryVisit { get; set; }

        [Required(ErrorMessage = "Entrez {0}")]
        [Display(Name = "Traitements medical")]
        public ICollection<int> IdMedicalTreatments { get; set; }

        [Display(Name = "Traitements medical")]
        public virtual ICollection<MedicalTreatment> MedicalTreatments { get; set; }
    }
}
