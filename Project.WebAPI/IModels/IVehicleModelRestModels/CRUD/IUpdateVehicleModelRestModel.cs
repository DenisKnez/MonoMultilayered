﻿using Project.Model.Common.IVehicleMakeDomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.IModels.IVehicleModelRestModels.CRUD
{
    public interface IUpdateVehicleModelRestModel
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }

        Guid VehicleMakeId { get; set; }
    }
}
