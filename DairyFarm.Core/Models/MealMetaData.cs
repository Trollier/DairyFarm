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

    }
}
