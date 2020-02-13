using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common.Interfaces
{
    public interface IPage<T>
    {
        List<T> Items { get; set; }

        int TotalPages { get; set; }

        int PageIndex { get; set; }

    }
}
