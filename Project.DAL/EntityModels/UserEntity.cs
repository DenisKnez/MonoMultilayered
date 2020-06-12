using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.EntityModels
{
    public class UserEntity : BaseEntity, IUserEntity
    {
        public string Name { get; set; }
    }
}
