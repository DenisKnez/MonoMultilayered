using Project.Model.Common.IVehicleMakeDomainModels;
using Project.Model.Common.IVehicleModelDomainModels;
using Project.Model.VehicleMakeDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.VehicleModelDomainModels
{
    public class VehicleModel : IVehicleModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public IVehicleMake VehicleMake { get; set; }

    }
}
