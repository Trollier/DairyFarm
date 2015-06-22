using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(FoodMetaData))]
    public partial class Food
    {

    }
    public class FoodMetaData
    {

        [Display(Name = "Quantité")]
        public decimal TotQuantity { get; set; }

        [Required(ErrorMessage = "Entrez le {0}")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Nom de la nourriture")]
        public string Label { get; set; }
    }
}
