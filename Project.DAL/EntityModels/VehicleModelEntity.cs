using Project.DAL.IDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.DomainModels
{
    public class VehicleModelEntity : BaseEntity, IVehicleModelEntity
    {
        public string Name { get; set; }

        public string Abrv { get; set; }

        public Guid VehicleMakeEntityId { get; set; }

        public VehicleMakeEntity VehicleMakeEntity { get; set; }

    }
}
