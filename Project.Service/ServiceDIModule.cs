using Ninject.Modules;
using Project.Service.Common;
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
        }
    }
}
