using System;
using System.Collections.Generic;
using System.Text;

namespace Project.WebAPI.Models.IVehicleMakeRestModels.CRUD
{
    public interface IUpdateVehicleMakeRestModel
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }

    }
}
