using Project.WebAPI.IModels.IVehicleModelRestModels.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Models.VehicleModelRestModels.CRUD
{
    public class UpdateVehicleModelRestModel : IUpdateVehicleModelRestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}