﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    public class UserParameters : Parameters, IUserParameters
    {
        public string Name { get; set; }

    }
}
