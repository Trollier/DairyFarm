using System;
using System.Collections.Generic;
using System.Data.Entity;
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

       #region Cattle
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

       public Cattle GetCattleById(int? id)
       {
           return _db.Cattles.Find(id);
       }

       public IEnumerable<Cattle> GetCattles()
       {
           return _db.Cattles;
       }

       public IQueryable<IGrouping<int, Cattle>> IndexCattle()
       {
          return  _db.Cattles.Where(c => c.Active != true).GroupBy(c => c.IdHerd);
       }
       #endregion
       #region DiseaseHistory
       public bool AddDiseasesHistory(DiseasesHistory diseasesHistory)
       {
           try
           {
               _db.DiseasesHistories.Add(diseasesHistory);
               _db.SaveChanges();
               return true;
           }
           catch
           {
               return false;
           }

       }
       #endregion
       #region MedicalTreatments
       public IEnumerable<MedicalTreatment> GetMedicalTreatments()
       {
           return _db.MedicalTreatments;
       }
       public MedicalTreatment GetMedicalTreatmentById(int? id)
       {
           return _db.MedicalTreatments.Find(id);
       }
       #endregion
       #region Gestation
       public bool AddDGestation(Gestation gestation)
       {
           try
           {
               _db.Gestations.Add(gestation);
               _db.SaveChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }
       #endregion
       #region Disease
       public IEnumerable<Disease> GetDiseases()
       {
           return _db.Diseases;

       }

       public Disease GetDiseaseById(int? id)
       {
           return _db.Diseases.Find(id);
       }

       public bool GetDiseaseContagious(int? id)
       {
           return _db.Diseases.Find(id).Contagious;
       }
       public IEnumerable<Cattle> GetCattlesByHerd(int idHerd)
       {
           return _db.Cattles.Where(c => c.IdHerd == idHerd).ToList();
       }
       #endregion
       #region CattleType
       public IEnumerable<CattleType> GetCattleTypes()
       {
           return _db.CattleTypes;
       }
       #endregion
       #region Herd
       public IEnumerable<Herd> GetHerds()
       {
           return _db.Herds;
       }
       public IEnumerable<Herd> GetHerdById(int idHerd)
       {
           var herdSelect = _db.Herds.Find(idHerd);
           return _db.Herds.Where(c => c.CattleType.Rank >= herdSelect.CattleType.Rank && c.CattleType.Sex == herdSelect.CattleType.Sex).ToList();

       }
       #endregion
       #region CattleProduction
       public IEnumerable<CattleProduction> GetCattleProductions()
       {
           return _db.CattleProductions.Include(c => c.Cattle);
       }

       public bool AddCattleProduction(CattleProduction cattleProduction)
       {
           try
           {
               _db.CattleProductions.Add(cattleProduction);
               _db.SaveChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }

       public CattleProduction GetCattleProductionById(int? id)
       {
           return _db.CattleProductions.Find(id);
       }
       #endregion
       #region Food
       #endregion
       #region Season
       #endregion
       #region Meal
       #endregion













    
    }
}
