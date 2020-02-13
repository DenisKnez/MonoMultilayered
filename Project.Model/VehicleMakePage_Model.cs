using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public class VehicleMakePage_Model : IVehicleMakePage_Model
    {
        public List<IVehicleMakeModel_Model> Items { get; set; }

        public int TotalPages { get; set; }

        public int PageIndex { get; set; }

        public string SearchString { get; set; }

        public string SortOrder { get; set; }

    }
}
