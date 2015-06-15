using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DairyFarm.Core.DAL;

namespace DairyFarm.Web.Models
{
    public class ChangeHerdViewModel
    {
        public IEnumerable<int> IdChangeCattle { get; set; }
        public int IdChangeHerd { get; set; }
    }
}