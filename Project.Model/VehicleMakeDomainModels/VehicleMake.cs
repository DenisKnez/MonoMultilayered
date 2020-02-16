using Project.Model.Common.IVehicleMakeDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.VehicleMakeDomainModels
{
    public class VehicleMake : IVehicleMake
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }
    }
}
