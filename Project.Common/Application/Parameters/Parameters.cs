using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    public abstract class Parameters : IParameters
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


        #region Filters

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public bool IsActive { get; set; }

        public Guid Id { get; set; }

        #endregion


    }
}
