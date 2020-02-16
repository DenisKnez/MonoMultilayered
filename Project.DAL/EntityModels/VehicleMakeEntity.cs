using Project.DAL.IDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.DomainModels
{
    public class VehicleMakeEntity : BaseEntity, IVehicleMakeEntity
    {
        public string Name { get; set; }

        public string Abrv { get; set; }

        // navigation property
        public List<VehicleModelEntity> VehicleModels { get; set; }
        
    }
}
