using Project.Model.Common.IVehicleMakeDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Common.IVehicleModelDomainModels.CRUD
{
    public interface ICreateVehicleModel
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }

        Guid VehicleMakeId { get; set; }
    }
}
