using Project.DAL.IDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.DomainModels
{
    public class VehicleModel : IVehicleModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public Guid VehicleMakeId { get; set; }

        public VehicleMake VehicleMake { get; set; }

    }
}
