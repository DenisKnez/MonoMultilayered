using Project.Model.Common.IVehicleMakeDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.WebAPI.Models.IVehicleMakeRestModels
{
    public interface IVehicleMakePageRestModel
    {
        List<IVehicleMakeRestModel> Items { get; set; }

        int TotalPages { get; set; }

        int PageIndex { get; set; }

        string SearchString { get; set; }

        string SortOrder { get; set; }

    }
}
