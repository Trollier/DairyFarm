using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DairyFarm.Web.Models
{
    public class NewGestationModel
    {
        public int idCattleInGestation { get; set; }
        public int IdGestation { get; set; }
        public int IdCattle { get; set; }
        public System.DateTime StartDateGestation { get; set; }
        public Nullable<System.DateTime> EndDateGestation { get; set; }
        public Nullable<System.DateTime> DateCalve { get; set; }
        public string CalveSex { get; set; }
        public string Comment { get; set; }
        public Nullable<bool> DeathCalve { get; set; }
    }
}