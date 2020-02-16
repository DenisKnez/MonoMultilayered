using System;
using System.Collections.Generic;
using System.Text;
using Project.Model.Common.IVehicleMakeDomainModels;

namespace Project.Model.Common.IVehicleModelDomainModels
{
    public interface IVehicleModel
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }

        //Guid VehicleMakeId { get; set; }

        IVehicleMake VehicleMake { get; set; }

    }
}
