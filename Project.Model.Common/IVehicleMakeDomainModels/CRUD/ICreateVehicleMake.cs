using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Common.IVehicleMakeDomainModels.CRUD
{
    public interface ICreateVehicleMake
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }

    }
}
