using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(DietMetaData))]
    public partial class Diet
    {
        public ICollection<int> IdCattleTypes { get; set; }
        public ICollection<int> IdFoods { get; set; }
    }
    public class DietMetaData
    {
    }
}
