using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DairyFarm.DAL;

namespace DairyFarm.Services
{
    public class ServiceGestationt : IDisposable
    {

        private readonly DairyFarmEntities _db = new DairyFarmEntities();

        public bool CreateGestation(Gestation gestation)
        {
            try
            {
                    _db.Gestations.Add(gestation);
                    _db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        public bool EditGestation(Gestation gestation)
        {
            try
            {
                    _db.Entry(gestation).State = EntityState.Modified;
                    _db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteGestation(int id)
        {
            try
            {
                    Gestation gestation = _db.Gestations.Find(id);
                    _db.Gestations.Remove(gestation);
                    _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }


        public void Dispose()
        {
            _db.Dispose();
        }
    }
}