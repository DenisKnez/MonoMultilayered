using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.IModels.IVehicleModelRestModels
{
    public interface IVehicleModelPageRestModel
    {
        List<IVehicleModelRestModel> Items { get; set; }

        int TotalPages { get; set; }

        int PageIndex { get; set; }

        string SearchString { get; set; }

        string SortOrder { get; set; }


    }
}
