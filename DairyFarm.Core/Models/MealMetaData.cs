using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(MealMetaData))]
    public partial class Meal
    {
        public string Hours { get; set; }
        public List<Food> FoodExhausted { get; set; }
        public List<Meal> givenFood { get; set; }
    }
    public class MealMetaData
    {
        [Required(ErrorMessage = "Select une {0}")]
        [Display(Name = "Nourriture")]
        public int IdFood { get; set; }

        [Required(ErrorMessage = "Select un {0}")]
        [Display(Name = "Troupeau")]
        public int IdHerd { get; set; }

        [Required(ErrorMessage = "Select une {0}")]
        [Display(Name = "Date de repas")]
        [DataType(DataType.Text)]
        public System.DateTime DateMeal { get; set; }

        [Required(ErrorMessage = "Select une {0}")]
        [Display(Name = "Hour de repas")]
        public string Hours { get; set; }

        [Required(ErrorMessage = "Select une {0}")]
        [Display(Name = "Quantité")]
        public decimal Quantity { get; set; }
    }
}
