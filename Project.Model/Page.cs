using Project.DAL.IDomainModels;
using Project.Model.Common;
using Project.Model.Common.IVehicleMakeDomainModels;
using Project.Model.VehicleMakeDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public class Page<T> : IPage<T>
    {
        public List<T> Items { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
