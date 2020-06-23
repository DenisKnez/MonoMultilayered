using System;
using System.Collections.Generic;

namespace Project.DAL.EntityModels
{
    public partial class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool? IsActive { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal? Salary { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
