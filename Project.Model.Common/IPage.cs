using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Common
{
    public interface IPage<T>
    {
        List<T> Items { get; set; }

        string SearchString { get; set; }

        string SortOrder { get; set; }

        int PageIndex { get; set; }

        int TotalPages { get; set; }


    }
}
