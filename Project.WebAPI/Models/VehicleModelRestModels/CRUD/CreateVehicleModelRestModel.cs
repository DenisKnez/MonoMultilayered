﻿using Project.Model.Common.IVehicleMakeDomainModels;
using Project.Model.VehicleMakeDomainModels;
using Project.WebAPI.IModels.IVehicleModelRestModels.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Models.VehicleModelRestModels.CRUD
{
    public class CreateVehicleModelRestModel : ICreateVehicleModelRestModel
    {
        public string Name { get; set; }

        public string Abrv { get; set; }

        public Guid VehicleMakeId { get; set; }
    } 
}

