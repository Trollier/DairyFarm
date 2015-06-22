using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(MedicalTreatmentMetaData))]
    public partial class MedicalTreatment
    {

    }
    public class MedicalTreatmentMetaData
    {

        [Required(ErrorMessage = "Entrez le {0}")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Nom du medicament")]
        public string Label { get; set; }
    }
}
