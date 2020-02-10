using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public class VehicleMakePage_Model : IVehicleMakePage_Model
    {
        public List<VehicleMakeModel_Model> VehicleMakeModel_Models { get; set; }

        public int TotalPages { get; set; }

        public int PageIndex { get; set; }

    }
}
