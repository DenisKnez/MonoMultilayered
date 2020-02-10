using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    public interface IPageList<T>
    {
        List<T> Items { get; set; }

        int TotalPages { get; set; }

        int PageIndex { get; set; }

    }
}
