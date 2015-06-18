using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(CattleTypeMetaData))]
    public partial class CattleType
    {

    }
    public class CattleTypeMetaData
    {
        [Required(ErrorMessage = "Select un {0}")]
        [Display(Name = "Type de la bête")]
        public string Label { get; set; }
        [Required(ErrorMessage = "Select un {0}")]
        [Display(Name = "Sexe")]
        public string Sex { get; set; }
    }
}
