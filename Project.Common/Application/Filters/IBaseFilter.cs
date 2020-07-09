using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// Every filter needs to inherit this interface, it provides properties that all entities have
    /// </summary>
    public interface IBaseFilter
    {
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public bool? IsActive { get; set; }

        public Guid Id { get; set; }

    }
}
