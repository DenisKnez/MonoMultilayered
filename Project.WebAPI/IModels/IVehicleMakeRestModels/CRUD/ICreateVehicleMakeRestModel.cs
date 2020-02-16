using System;
using System.Collections.Generic;
using System.Text;

namespace Project.WebAPI.Models.IVehicleMakeRestModels.CRUD
{
    public interface ICreateVehicleMakeRestModel
    {
        string Name { get; set; }

        string Abrv { get; set; }
    }
}
