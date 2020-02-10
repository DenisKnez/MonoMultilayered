using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    public interface IPageSettings
    {
        int PageIndex { get; set; }

        string SearchString { get; set; }

        string SortOrder { get; set; }

        int TotalPages { get; set; }

        int PageSize { get; set; }

    }
}
