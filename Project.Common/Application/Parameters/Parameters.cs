using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// Abstract class used to pass parameters through the layers
    /// </summary>
    /// <typeparam name="TFilter"></typeparam>
    public abstract class Parameters<TFilter> : IParameters<TFilter>
    {
        protected int maxPageSize = 50;
        public virtual int PageNumber { get; set; } = 1;

        protected int pageSize = 10;
        public virtual int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string OrderBy { get; set; }

        public string Fields { get; set; }

        public TFilter Filter { get; set; }

    }
}
