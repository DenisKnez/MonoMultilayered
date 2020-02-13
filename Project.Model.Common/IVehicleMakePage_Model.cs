using System;
using System.Collections.Generic;
using System.Text;
using Project.Model.Common;

namespace Project.Model
{
    public interface IVehicleMakePage_Model
    {
        List<IVehicleMakeModel_Model> Items { get; set; }

        int TotalPages { get; set; }

        int PageIndex { get; set; }

        string SearchString { get; set; }

        string SortOrder { get; set; }

    }
}
