using Project.DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.IDomainModels
{
    public interface IVehicleMakeEntity : IBaseEntity
    {
        string Name { get; set; }

        string Abrv { get; set; }

        // navigation property
        List<VehicleModelEntity> VehicleModels { get; set; }

    }
}
