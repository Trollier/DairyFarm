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
        #region Cattle
        //Cattle
        bool CreateCattle(object o);
        bool EditCattle(object o);
        bool DeleteCattle(object o);
        List<Cattle> GetCattle();
        #endregion 
        //CattleProduction
        bool CreateCattleProduction(object o);
        bool EditCattleProduction(object o);
        bool DeleteCattleProduction(object o);
        List<CattleProduction> GetCattleProduction();
        //

    }
}