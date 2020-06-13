using Project.Model.Common.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.System
{
    public class BaseModel : IBaseModel
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public bool IsActive { get; set; }
    }
}
