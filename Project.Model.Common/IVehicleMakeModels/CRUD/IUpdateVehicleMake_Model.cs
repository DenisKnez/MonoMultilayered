using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Common.IVehicleMakeModels.CRUD
{
    public interface IUpdateVehicleMake_Model
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }

    }
}
