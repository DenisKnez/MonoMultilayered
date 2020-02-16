using Project.Model.Common;
using Project.WebAPI.Models.IVehicleMakeRestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.WebAPI.Models.VehicleMakeRestModels
{
    public class VehicleMakeRestModel : IVehicleMakeRestModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

    }
}
