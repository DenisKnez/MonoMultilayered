using Project.Model.Common.IVehicleModelDomainModels.CRUD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.VehicleModelDomainModels.CRUD
{
    public class UpdateVehicleModel : IUpdateVehicleModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public Guid VehicleMakeId { get; set; }
    }
}
