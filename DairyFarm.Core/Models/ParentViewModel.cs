using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DairyFarm.Core.Models
{

    public class ParentViewModel
    {
        public int IdCattle { get; set; }

        [Display(Name = "Parent male")]
        public int? MalParent { get; set; }

        [Display(Name = "Parent femelle")]
        public int? FemaleParent { get; set; }
    }
}
