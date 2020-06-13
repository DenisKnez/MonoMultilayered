using Project.Model.Common;
using Project.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public class UserModel : BaseModel, IUserModel
    {
        public string Name { get; set; }
    }
}
