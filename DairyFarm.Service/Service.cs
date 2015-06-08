using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DairyFarm.Core.DAL;


namespace DairyFarm.Service
{
   public class DairyFarmService : IDairyFarmService
    {
       private readonly DairyFarmEntities _db;
       public DairyFarmService(DairyFarmEntities db)
       {
            this._db = db;
       }

       public bool AddCattle(Cattle cattle)
       {           
           try
           {
               _db.Cattles.Add(cattle);
               _db.SaveChanges();
               return true;
           }
           catch
           {
               return false;

           }
           
       }
    }
}
