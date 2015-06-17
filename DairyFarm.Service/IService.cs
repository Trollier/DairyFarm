﻿using System;
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
        bool AddCattleProduction(CattleProduction cattle);
        bool AddDiseasesHistory(DiseasesHistory diseasesHistory);
        bool AddDGestation(Gestation gestation);
        bool EditCattle(Cattle cattle);
        bool EditCattleProductions(CattleProduction cattleProduction);
        bool DeleteCattle(int id);
        bool EditHerd(Herd herd);
        Cattle GetCattleById(int? id);
        CattleProduction GetCattleProductionById(int? id);
        MedicalTreatment GetMedicalTreatmentById (int? id);
        Disease GetDiseaseById(int ?id);
        Herd GetHerdById(int idHerd);
        bool GetDiseaseContagious(int? id);
        IEnumerable<Disease> GetDiseases();
        IEnumerable<CattleType> GetCattleTypes();
        IEnumerable<Herd> GetHerds();
        IEnumerable<CattleProduction> GetCattleProductions();
        IEnumerable<MedicalTreatment> GetMedicalTreatments();
        IEnumerable<Cattle> GetCattles();
        IEnumerable<Cattle> GetCattlesByHerd(int idHerd);
        IEnumerable<Herd> GetHerdListById(int idHerd);
        IEnumerable<Cattle> GetCattleInQuarantine();
        IEnumerable<CattleProduction> GetYesterdayProd(DateTime date);
        IEnumerable<CattleProduction> GetProductionsByDate(DateTime date);
        IEnumerable<CattleProduction> GetProductions();
        IQueryable<IGrouping<int, Cattle>> IndexCattle();
        void DecrementHerd(int id);
        void IncrementHerd(int id);

    }
}
