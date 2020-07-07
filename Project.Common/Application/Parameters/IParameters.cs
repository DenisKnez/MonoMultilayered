using System;

namespace Project.Common
{
    public interface IParameters<TFilter>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string Fields { get; set; }
        public TFilter Filter { get; set; }
    }
}