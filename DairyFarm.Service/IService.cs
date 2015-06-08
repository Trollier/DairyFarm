using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DairyFarm.Core.DAL;

namespace DairyFarm.Service
{
    public interface IDairyFarmService
    {
       bool AddCattle(Cattle cattle);
    }
}
