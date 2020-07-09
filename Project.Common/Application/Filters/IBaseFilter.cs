using System;

namespace Project.Common
{
    /// <summary>
    /// Every filter needs to inherit this class, it provides properties that all entities have
    /// </summary>
    public class BaseFilter
    {
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public bool? IsActive { get; set; }

        public Guid Id { get; set; }
    }
}