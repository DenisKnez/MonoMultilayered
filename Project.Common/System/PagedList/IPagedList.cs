using System.Collections.Generic;

namespace Project.Common.System
{
    public interface IPagedList<T> : IList<T>
    {
        int CurrentPage { get; }
        int TotalPages { get; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
        bool HasPrevious { get; }
        bool HasNext { get; }
    }
}