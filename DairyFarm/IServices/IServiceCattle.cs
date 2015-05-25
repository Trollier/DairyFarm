using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DairyFarm.DAL;

namespace DairyFarm.IServices
{
    public interface IServiceCattle : IDisposable
    {

        bool CreateCattle(Cattle cattle);
        bool EditCattle(Cattle cattle);
        bool DeleteCattle(Cattle cattle);

    }
}