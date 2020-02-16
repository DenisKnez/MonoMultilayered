using Project.DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.IDomainModels
{
    public interface IVehicleModelEntity
    {
        string Name { get; set; }

        string Abrv { get; set; }

        //Guid VehicleMakeEntityId { get; set; }

        VehicleMakeEntity VehicleMakeEntity { get; set; }
    }
}
