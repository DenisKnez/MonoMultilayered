using System;
using System.Collections.Generic;

namespace Project.DAL.EntityModels
{
    public partial class UserEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool? IsActive { get; set; }
    }
}
