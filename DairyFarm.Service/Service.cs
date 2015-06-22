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


        /* ------------------------ Cattle --------------------------------*/
        #region Cattle

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
            return _db.Cattles.Where(c => c.Active == true && c.Herd.Active == true).GroupBy(c => c.IdHerd);
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

        public bool AddCattle(Cattle cattle)
        {
            try
            {
                cattle.Active = true;
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
        #endregion Cattle

        /* ------------------------ Herds --------------------------------*/
        #region Herds
        public Herd GetHerdById(int idHerd)
        {
            return _db.Herds.Find(idHerd);
        }
        public IEnumerable<Herd> GetHerds()
        {
            return _db.Herds.Where(h => h.MaxAnimals - h.AvailablePlaces != 0);
        }

        public IEnumerable<Herd> GetHerdListById(int idHerd)
        {
            var herdSelect = _db.Herds.Find(idHerd);
            return _db.Herds.Where(c => c.CattleType.Rank >= herdSelect.CattleType.Rank && c.CattleType.Sex == herdSelect.CattleType.Sex && c.IdHerd != herdSelect.IdHerd && herdSelect.AvailablePlaces != 0 && c.Active == true).ToList();
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
        public bool DeleteCHerd(int id)
        {
            throw new NotImplementedException();
        }
        #endregion Herds

        /* ------------------------ Diseases --------------------------------*/
        #region Diseases
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
            return _db.Cattles.Where(c => c.IdHerd == idHerd && c.Active == true).ToList();
        }

        public bool AddDisease(Disease disease)
        {
            try
            {

                _db.Diseases.Add(disease);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }

        public bool EditDisease(Disease disease)
        {
            try
            {
                _db.Entry(disease).State = EntityState.Modified;
                _db.SaveChanges();
                return true;

            }
            catch
            {
                return false;

            }
        }

        public bool DeleteDisease(int id)
        {
            throw new NotImplementedException();
        }
        #endregion Diseases
        /* ------------------------ DiseasesHistory --------------------------------*/
        #region DiseasesHistory
        public DiseasesHistory GetDiseasesHistoryById(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DiseasesHistory> GetDiseasesHistories()
        {
            throw new NotImplementedException();
        }

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
        public bool EditDiseasesHistory(DiseasesHistory diseasesHistory)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCDiseasesHistory(int id)
        {
            throw new NotImplementedException();
        }

        #endregion DiseasesHistory

        /* ------------------------ MedicalTreatment --------------------------------*/
        #region MedicalTreatment
        public IEnumerable<MedicalTreatment> GetMedicalTreatments()
        {
            return _db.MedicalTreatments;
        }
        public MedicalTreatment GetMedicalTreatmentById(int? id)
        {
            return _db.MedicalTreatments.Find(id);
        }


        public bool AddMedicalTreatment(MedicalTreatment medicalTreatment)
        {
            try
            {
                _db.MedicalTreatments.Add(medicalTreatment);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }

        public bool EditMedicalTreatment(MedicalTreatment medicalTreatment)
        {
            try
            {
                _db.Entry(medicalTreatment).State = EntityState.Modified;
                _db.SaveChanges();
                return true;

            }
            catch
            {
                return false;

            }
        }

        public bool DeleteCMedicalTreatment(int id)
        {
            throw new NotImplementedException();
        }
        #endregion MedicalTreatment

        /* ------------------------ Gestations --------------------------------*/
        #region Gestations
        public Gestation GetGestationById(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Gestation> GetGestations()
        {
            throw new NotImplementedException();
        }

        public bool AddGestation(Gestation gestation)
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
        public bool EditGestation(Gestation gestation)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCGestation(int id)
        {
            throw new NotImplementedException();
        }
        #endregion Gestations

        /* ------------------------ Food --------------------------------*/
        #region Food
        public Food GetFoodById(int? id)
        {
            return _db.Foods.Find(id);
        }
        public IEnumerable<Food> GetFoods()
        {
            return _db.Foods.ToList();
        }

        public bool AddFood(Food food)
        {
            try
            {

                _db.Foods.Add(food);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }

        public bool EditFood(Food food)
        {
            try
            {
                _db.Entry(food).State = EntityState.Modified;
                _db.SaveChanges();
                return true;

            }
            catch
            {
                return false;

            }
        }

        public bool DeleteFood(int id)
        {
            try
            {
                Food food = _db.Foods.Find(id);
                _db.Foods.Remove(food);
                _db.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }
        public IEnumerable<Food> FoodExhausted()
        {
            return _db.Foods.Where(f => f.TotQuantity == 0);
        }
        #endregion Food

        /* ------------------------ CattleProductions --------------------------------*/
        #region CattleProduction
        public CattleProduction GetCattleProductionById(int? id)
        {
            return _db.CattleProductions.Find(id);
        }
        public IEnumerable<CattleProduction> GetCattleProductions()
        {
            return _db.CattleProductions.Include(c => c.Cattle);
        }


        public IEnumerable<CattleProduction> GetYesterdayProd(DateTime date)
        {
            var yesterday = DateTime.Today.AddDays(-1);
            return _db.CattleProductions.Where(c => c.Dateprod.Month == yesterday.Month && c.Dateprod.Day == yesterday.Day).ToList();
        }
        public IEnumerable<CattleProduction> GetProductionsByDate(DateTime date)
        {
            return _db.CattleProductions.Where(d => d.Dateprod.Month == date.Month && d.Dateprod.Day == date.Day && d.Cattle.Active == true);
        }
        public IEnumerable<CattleProduction> GetProductions()
        {
            return _db.CattleProductions.Where(d => d.Cattle.Active == true);
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
        public bool DeleteCattleProduction(int id)
        {
            throw new NotImplementedException();
        }
        #endregion CattleProductions

        /* ------------------------ Meals --------------------------------*/
        #region Meals
        public Meal GetMealById(int? id)
        {
            return _db.Meals.Find(id);
        }

        public IEnumerable<Meal> GetMeals()
        {
            return _db.Meals.Include(m => m.Food).Include(m => m.Herd);
        }


        public bool AddMeal(Meal meal)
        {
            try
            {

                _db.Meals.Add(meal);
                _db.SaveChanges();
                var food = GetFoodById(meal.IdFood);
                food.TotQuantity -= meal.Quantity;
                EditFood(food);
                return true;
            }
            catch
            {
                return false;

            }
        }

        public bool EditMeal(Meal meal)
        {
            try
            {
                _db.Entry(meal).State = EntityState.Modified;
                _db.SaveChanges();
                return true;

            }
            catch
            {
                return false;

            }
        }

        public bool DeleteMeal(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Meal> GivenFood(DateTime date)
        {
            return _db.Meals.Where(m => m.DateMeal.Day == date.Day && m.DateMeal.Month == date.Month && m.DateMeal.Year == date.Year).ToList();
        }
        #endregion Meals

        /* ------------------------ Diets --------------------------------*/
        #region Diets
        public Diet GetDietById(int? id)
        {
            return _db.Diets.Find(id);
        }

        public IEnumerable<Diet> GetDiets()
        {
            return _db.Diets.Include(d => d.Season);
        }

        public bool AddDiet(Diet diet)
        {
            try
            {

                _db.Diets.Add(diet);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }

        public bool EditDiet(Diet diet)
        {
            try
            {
                _db.Entry(diet).State = EntityState.Modified;
                _db.SaveChanges();
                return true;

            }
            catch
            {
                return false;

            }
        }

        public bool DeleteDiet(int id)
        {
            throw new NotImplementedException();
        }

        #endregion Diets

        /* ------------------------ Seasons --------------------------------*/
        #region Seasons
        public Season GetSeasonById(int? id)
        {
            return _db.Seasons.Find(id);
        }

        public IEnumerable<Season> GetSeasons()
        {
            return _db.Seasons.ToList();
        }


        public bool AddSeason(Season season)
        {
            try
            {

                _db.Seasons.Add(season);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }

        public bool EditSeason(Season season)
        {
            try
            {
                _db.Entry(season).State = EntityState.Modified;
                _db.SaveChanges();
                return true;

            }
            catch
            {
                return false;

            }
        }

        public bool DeleteSeason(int id)
        {
            try
            {
                Season season = _db.Seasons.Find(id);
                _db.Seasons.Remove(season);
                _db.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }
        #endregion Seasons

        /* ------------------------ CattleTypes --------------------------------*/
        #region CattleTypes
        public CattleType GetCattleTypeById(int? id)
        {
            return _db.CattleTypes.Find(id);
        }
        public IEnumerable<CattleType> GetCattleTypes()
        {
            return _db.CattleTypes.ToList();
        }


        public bool AddCattleType(CattleType cattleType)
        {
            try
            {

                _db.CattleTypes.Add(cattleType);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }

        public bool EditCattleType(CattleType cattleType)
        {
            try
            {
                _db.Entry(cattleType).State = EntityState.Modified;
                _db.SaveChanges();
                return true;

            }
            catch
            {
                return false;

            }
        }

        #endregion CattleTypes



        public bool DeleteCattleType(int id)
        {
            throw new NotImplementedException();
        }


        public Diet getDietByDate(DateTime date, int id)
        {
           var herd = GetHerdById(id);
           var diets = _db.Diets.Where(m => m.Season.StartDate.Month <= date.Month
                && m.Season.StartDate.Day <= date.Day
                && m.Season.EndDate.Month > date.Month
                && m.Season.EndDate.Day > date.Day
                );
            foreach (var diet in diets)
            {
                if (diet.CattleTypes.Contains(herd.CattleType))
                {
                    return diet;
                }
            }
            return null;
        }




      

    }
}
