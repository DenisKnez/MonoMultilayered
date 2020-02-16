using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
