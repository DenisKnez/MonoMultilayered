using Project.WebAPI.IModels.IVehicleModelRestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Models.VehicleModelRestModels
{
    public class VehicleModelRestModel : IVehicleModelRestModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public Guid VehicleMakeId { get; set; }

    }

}