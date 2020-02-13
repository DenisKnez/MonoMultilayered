using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.IDomainModels
{
    public interface IVehicleModel : IBaseEntity
    {
        string Name { get; set; }

        string Abrv { get; set; }

        Guid VehicleMakeId { get; set; }
    }
}
