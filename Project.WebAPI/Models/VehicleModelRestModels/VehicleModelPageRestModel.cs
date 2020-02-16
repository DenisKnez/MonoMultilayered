using Project.WebAPI.IModels.IVehicleModelRestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Models.VehicleModelRestModels
{
    public class VehicleModelPageRestModel : IVehicleModelPageRestModel
    {
        public List<IVehicleModelRestModel> Items { get; set; }
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }
    }
}
