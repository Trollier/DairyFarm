using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DairyFarm.Core.DAL;

namespace DairyFarm.Web.Models
{
    public class ChangeHerdViewModel
    {
        [Required(ErrorMessage = "Select un {0}")]
        [Display(Name = "Bête(s)")]
        public IEnumerable<int> IdChangeCattle { get; set; }

        [Required(ErrorMessage = "Select un {0}")]
        [Display(Name = "Troupeau")]
        public int IdChangeHerd { get; set; }
    }
}