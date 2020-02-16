using Project.Model.Common.IVehicleMakeDomainModels;
using Project.WebAPI.Models.IVehicleMakeRestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.WebAPI
{
    public class VehicleMakePageRestModel : IVehicleMakePageRestModel
    {
        public List<IVehicleMakeRestModel> Items { get; set; }

        public int TotalPages { get; set; }

        public int PageIndex { get; set; }

        public string SearchString { get; set; }

        public string SortOrder { get; set; }

    }
}
