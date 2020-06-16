using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common.Application
{
    public class Parameters : IParameters
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

    }
}
