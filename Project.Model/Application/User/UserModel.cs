using System;

namespace Project.Model
{
    public class UserModel : BaseModel, IUserModel
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public decimal? Salary { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime DateJoined { get; set; }

        public Guid CompanyId { get; set; }

        public CompanyModel Company { get; set; }
    }
}