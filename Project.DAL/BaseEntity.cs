using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public bool IsActive { get; set; }
    }
}
