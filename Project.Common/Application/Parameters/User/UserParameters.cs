using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common.Application
{
    public class UserParameters : Parameters, IUserParameters
    {
        public string Name { get; set; }

    }
}
