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
               var herd = GetHerdById(cattle.IdHerd);
               herd.AvailablePlaces--;
               EditHerd(herd);
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
          return  _db.Cattles.Where(c => c.Active != true ).GroupBy(c => c.IdHerd);
       }
       public bool EditCattle(Cattle cattle)
       {
           try
           {
               _db.Entry(cattle).State = EntityState.Modified;
               _db.SaveChanges();
               return true;

           }
           catch
           {
               return false;

           }
       }

       public bool DeleteCattle(int id)
       {
           var cattle = GetCattleById(id);
           cattle.Active = true;
           return EditCattle(cattle);
       }

       public IEnumerable<Cattle> GetCattleInQuarantine()
       {
           return _db.Cattles.Where(c => c.InQuarantine == true).ToList();
       }

       public IEnumerable<Cattle> GetCattlesMilk()
       {
           try
           {
               return _db.Cattles.Where(c => c.Herd.IdCattleType == 3 && c.Active == true);
           }
           catch (Exception)
           {

               return null;
           }
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
           return _db.Cattles.Where(c => c.IdHerd == idHerd && c.Active==false).ToList();
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
           return _db.Herds.Where(h=>h.MaxAnimals-h.AvailablePlaces != 0);
       }
       public Herd GetHerdById(int idHerd)
       {
           return  _db.Herds.Find(idHerd);
       }
       public IEnumerable<Herd> GetHerdListById(int idHerd)
       {
           var herdSelect = _db.Herds.Find(idHerd);
           return _db.Herds.Where(c => c.CattleType.Rank >= herdSelect.CattleType.Rank && c.CattleType.Sex == herdSelect.CattleType.Sex && c.IdHerd!=herdSelect.IdHerd && herdSelect.AvailablePlaces!=0).ToList();
       }
       public bool EditHerd(Herd herd)
       {
           try
           {
               _db.Entry(herd).State = EntityState.Modified;
               _db.SaveChanges();
               return true;

           }
           catch
           {
               return false;

           }
       }
       public void DecrementHerd(int id)
       {
           var herd = GetHerdById(id);
           herd.AvailablePlaces--;
           EditHerd(herd);
       }
       public void IncrementHerd(int id)
       {
           var herd = GetHerdById(id);
           herd.AvailablePlaces++;
           EditHerd(herd);
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
       public IEnumerable<CattleProduction> GetYesterdayProd(DateTime date)
       {
           var yesterday = DateTime.Today.AddDays(-1);
           return _db.CattleProductions.Where(c => c.Dateprod.Month == yesterday.Month && c.Dateprod.Day == yesterday.Day).ToList();
       }
       public bool EditCattleProductions(CattleProduction cattleProduction)
       {
           try
           {
               _db.Entry(cattleProduction).State = EntityState.Modified;
               _db.SaveChanges();
               return true;

           }
           catch
           {
               return false;

           }
       }
       public IEnumerable<CattleProduction> GetProductionsByDate(DateTime date)
       {
           return _db.CattleProductions.Where(d => d.Dateprod.Month == date.Month && d.Dateprod.Day == date.Day &&  d.Cattle.Active==true);
       }
       public IEnumerable<CattleProduction> GetProductions()
       {
           return _db.CattleProductions.Where(d=>d.Cattle.Active == true);
       }

       public bool AddHerd(Herd herd)
       {
           try
           {

               _db.Herds.Add(herd);
               _db.SaveChanges();
               return true;
           }
           catch
           {
               return false;

           }
       }

       public IQueryable<Herd> GetHerdsIncludeCattle()
       {
           return _db.Herds.Where(c => c.Active == false).Include(h => h.CattleType);
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
