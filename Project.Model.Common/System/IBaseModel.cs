using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Common.System
{
    public interface IBaseModel
    {
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        Guid Id { get; set; }
        bool IsActive { get; set; }
    }

}
