using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(CattleProductionMetaData))]
    public partial class CattleProduction
    {
        public Nullable<decimal>   Quantity2 { get; set; }

    }
    public class CattleProductionMetaData
    {

    }
}
