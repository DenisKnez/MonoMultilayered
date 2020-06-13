using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime DateUpdated { get; set; }
        DateTime DateCreated { get; set; }
        bool? IsActive { get; set; }
    }
}
