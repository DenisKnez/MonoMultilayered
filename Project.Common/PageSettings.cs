using System;

namespace Project.Common
{
    public class PageSettings : IPageSettings
    {
        public int PageIndex { get; set; } = 0;

        public string SearchString { get; set; } = "";

        public string SortOrder { get; set; } = "";

        public int TotalPages { get; set; }

        public int PageSize { get; set; } = 5;

    }
}
