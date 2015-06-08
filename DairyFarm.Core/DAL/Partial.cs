using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DairyFarm.Core.DAL
{
    public partial class DiseasesHistory
    {
        public ICollection<int> IdMedicalTreatments { get; set; }
    }
}