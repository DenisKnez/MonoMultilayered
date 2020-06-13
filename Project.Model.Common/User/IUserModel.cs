using Project.Model.Common.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Common
{
    public interface IUserModel : IBaseModel
    {
        public string Name { get; set; }
    }
}
