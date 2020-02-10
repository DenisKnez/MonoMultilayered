using Project.DAL.IDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.DomainModels
{
    public class VehicleMake : IVehicleMake
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        // navigation property
        public List<VehicleModel> VehicleModels { get; set; }
        
    }
}
