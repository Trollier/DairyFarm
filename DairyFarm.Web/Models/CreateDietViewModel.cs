using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DairyFarm.Core.DAL;

namespace DairyFarm.Web.Models
{
    public class CreateDietViewModel
    {
        public int IdDiet { get; set; }
        public int IdSeason { get; set; }
        public string Label { get; set; }
        public ICollection<int> CattleTypes { get; set; }
        public ICollection<int> Foods { get; set; }
    }
}