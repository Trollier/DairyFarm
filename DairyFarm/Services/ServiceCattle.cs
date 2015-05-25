using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DairyFarm.DAL;
using DairyFarm.IServices;

namespace DairyFarm.Services
{
    public class ServiceCattle : IServiceCattle
    {
        private readonly DairyFarmEntities _db = new DairyFarmEntities();

        public bool CreateCattle(Cattle cattle)
        {
            try
            {
                    _db.Cattles.Add(cattle);
                    _db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }


        public bool EditCattle(Cattle cattle)
        {
            try
            {
                    _db.Entry(cattle).State = EntityState.Modified;
                    _db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteCattle(Cattle cattle)
        {

            cattle.Active = false;

            return EditCattle(cattle);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}