using System.Web.Mvc;
using DairyFarm.Core.DAL;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using DairyFarm.Service;

namespace DairyFarm.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IDairyFarmService, DairyFarmService>();
            container.RegisterType<DairyFarmEntities, DairyFarmEntities>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}