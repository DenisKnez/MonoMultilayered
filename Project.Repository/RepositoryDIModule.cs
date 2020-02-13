using Ninject.Modules;
using Project.Repository.Common;
using Project.Repository.Common.Repositories;
using Project.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Repository
{
    public class RepositoryDIModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            Kernel.Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            Kernel.Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
        }
    }
}
