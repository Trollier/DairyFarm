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
        /* ------------------------ Cattles --------------------------------*/
        #region Cattles
        Cattle GetCattleById(int? id);
        IEnumerable<Cattle> GetCattles();
        IEnumerable<Cattle> GetCattlesByHerd(int idHerd);
        IEnumerable<Cattle> GetCattleInQuarantine();
        IEnumerable<Cattle> GetCattlesMilk();
        IQueryable<IGrouping<int, Cattle>> IndexCattle();
        bool AddCattle(Cattle cattle);
        bool EditCattle(Cattle cattle);
        bool DeleteCattle(int id);
        #endregion Cattles

        /* ------------------------ Herds --------------------------------*/
        #region Herds
        Herd GetHerdById(int idHerd);
        IEnumerable<Herd> GetHerds();
        IEnumerable<Herd> GetHerdListById(int idHerd);
        IQueryable<Herd> GetHerdsIncludeCattle();
        void DecrementHerd(int id);
        void IncrementHerd(int id);
        bool AddHerd(Herd herd);
        bool EditHerd(Herd herd);
        bool DeleteCHerd(int id);
        #endregion Herds

        /* ------------------------ Diseases --------------------------------*/
        #region Diseases
        Disease GetDiseaseById(int? id);
        IEnumerable<Disease> GetDiseases();

        bool GetDiseaseContagious(int? id);

        bool AddDisease(Disease disease);
        bool EditDisease(Disease disease);
        bool DeleteCDisease(int id);
        #endregion Diseases

        /* ------------------------ DiseasesHistory --------------------------------*/
        #region DiseasesHistory
        DiseasesHistory GetDiseasesHistoryById(int? id);
        IEnumerable<DiseasesHistory> GetDiseasesHistories();

        bool AddDiseasesHistory(DiseasesHistory diseasesHistory);
        bool EditDiseasesHistory(DiseasesHistory diseasesHistory);
        bool DeleteCDiseasesHistory(int id);

        #endregion DiseasesHistory

        /* ------------------------ MedicalTreatment --------------------------------*/
        #region MedicalTreatment
        MedicalTreatment GetMedicalTreatmentById(int? id);
        IEnumerable<MedicalTreatment> GetMedicalTreatments();

        bool AddMedicalTreatment(MedicalTreatment medicalTreatment);
        bool EditMedicalTreatment(MedicalTreatment medicalTreatment);
        bool DeleteCMedicalTreatment(int id);
        #endregion MedicalTreatment


        /* ------------------------ Gestations --------------------------------*/
        #region Gestations
        Gestation GetGestationById(int? id);
        IEnumerable<Gestation> GetGestations();

        bool AddGestation(Gestation gestation);
        bool EditGestation(Gestation gestation);
        bool DeleteCGestation(int id);
        #endregion Gestations

        /* ------------------------ Food --------------------------------*/
        #region Food
        Food GetFoodById(int? id);
        IEnumerable<Food> GetFoods();

        bool AddFood(Food food);
        bool EditFood(Food food);
        bool DeleteFood(int id);
        #endregion Food 
        
        /* ------------------------ CattleProductions --------------------------------*/
        #region CattleProductions
        CattleProduction GetCattleProductionById(int? id);
        IEnumerable<CattleProduction> GetProductions();
        IEnumerable<CattleProduction> GetYesterdayProd(DateTime date);
        IEnumerable<CattleProduction> GetProductionsByDate(DateTime date);
        IEnumerable<CattleProduction> GetCattleProductions();
        bool AddCattleProduction(CattleProduction cattleProduction);
        bool EditCattleProductions(CattleProduction cattleProduction);
        bool DeleteCattleProduction(int id);
        #endregion CattleProductions

        /* ------------------------ Meals --------------------------------*/
        #region Meals
        Meal GetMealById(int? id);
        IEnumerable<Meal> GetMeals();

        bool AddMeal(Meal meal);
        bool EditMeal(Meal meal);
        bool DeleteMeal(int id);
        #endregion Meals

        /* ------------------------ Diets --------------------------------*/
        #region Diets
        Diet GetDietById(int? id);
        IEnumerable<Diet> GetDiets();

        bool AddDiet(Diet diet);
        bool EditDiet(Diet diet);
        bool DeleteDiet(int id);
        #endregion Diets

        /* ------------------------ Seasons --------------------------------*/
        #region Seasons
        Season GetSeasonById(int? id);
        IEnumerable<Season> GetSeasons();
        bool AddSeason(Season season);
        bool EditSeason(Season season);
        bool DeleteSeason(int id);
        #endregion Seasons

        /* ------------------------ CattleTypes --------------------------------*/
        #region CattleTypes
        CattleType GetCattleTypeById(int? id);
        IEnumerable<CattleType> GetCattleTypes();

        bool AddCattleType(CattleType cattleType);
        bool EditCattleType(CattleType cattleType);
        bool DeleteCattleType(int id);
        #endregion CattleTypes

    }
}
