using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public class VehicleMakeModel_Model : IVehicleMakeModel_Model
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        // treba biti list vehilceModel model
        //public List<VehicleModel> VehicleModels { get; set; }

    }
}
