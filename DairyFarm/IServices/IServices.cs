using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DairyFarm.DAL;

namespace DairyFarm.IServices
{
    public interface IServices : IDisposable
    {
        //Cattle
        bool CreateCattle(object o);
        bool EditCattle(object o);
        bool DeleteCattle(object o);
        List<Cattle> GetCattle();
        //CattleProduction
        bool CreateCattleProduction(object o);
        bool EditCattleProduction(object o);
        bool DeleteCattleProduction(object o);
        List<CattleProduction> GetCattleProduction();
        //
        bool Create(object o);
        bool Edit(object o);
        bool Delete(object o);
        bool Create(object o);
        bool Edit(object o);
        bool Delete(object o);
        bool Create(object o);
        bool Edit(object o);
        bool Delete(object o);
        bool Create(object o);
        bool Edit(object o);
        bool Delete(object o);
    }
}