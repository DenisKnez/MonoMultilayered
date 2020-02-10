using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public interface IVehicleMakePage_Model
    {
        List<VehicleMakeModel_Model> VehicleMakeModel_Models { get; set; }

        int TotalPages { get; set; }

        int PageIndex { get; set; }

    }
}
