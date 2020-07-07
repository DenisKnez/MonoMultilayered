using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common.Filters
{
    public interface ICompanyFilter : IBaseFilter
    {
        public string Name { get; set; }
    }
}
