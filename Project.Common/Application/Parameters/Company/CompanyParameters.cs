using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    public class CompanyParameters : Parameters, ICompanyParameters
    {
        public string Name { get; set; }

    }
}
