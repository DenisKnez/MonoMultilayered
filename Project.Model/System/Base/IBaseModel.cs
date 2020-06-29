using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public interface IBaseModel
    {
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        Guid Id { get; set; }
        bool IsActive { get; set; }
    }

}
