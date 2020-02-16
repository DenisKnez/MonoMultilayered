using Ninject.Modules;
using Project.Service.Common;
using Project.Service.Common.IVehicleMakeServices;
using Project.Service.Common.IVehicleModelServices;
using Project.Service.VehicleMakeServices;
using Project.Service.VehicleModelServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service
{
    public class ServiceDIModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IVehicleMakeService>().To<VehicleMakeService>();
            Kernel.Bind<IVehicleModelService>().To<VehicleModelService>();
        }
    }
}
