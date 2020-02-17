using Project.Model.Common.IVehicleMakeDomainModels.CRUD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.VehicleMakeDomainModels.CRUD
{
    public class UpdateVehicleMake : IUpdateVehicleMake
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }
    }
}
