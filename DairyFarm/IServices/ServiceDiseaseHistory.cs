﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DairyFarm.DAL;

namespace DairyFarm.Services
{
    public class ServiceDiseaseHistory : IDisposable
    {
        private readonly DairyFarmEntities _db = new DairyFarmEntities();

        public bool CreateDiseaseHistory(DiseasesHistory DiseaseHistory)
        {
            try
            {
                    _db.DiseasesHistories.Add(DiseaseHistory);
                    _db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        public bool EditDiseaseHistory(DiseasesHistory DiseaseHistory)
        {
            try
            {
                    _db.Entry(DiseaseHistory).State = EntityState.Modified;
                    _db.SaveChanges();   
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteDiseaseHistory(int id)
        {
            try
            {
                    DiseasesHistory diseaseHistory = _db.DiseasesHistories.Find(id);
                    diseaseHistory.MedicalTreatments.Clear();
                    _db.SaveChanges();
                    _db.DiseasesHistories.Remove(diseaseHistory);
                    _db.SaveChanges();
            }
            catch (Exception)
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