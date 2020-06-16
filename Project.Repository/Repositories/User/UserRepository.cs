using Project.Common.Application;
using Project.DAL;
using Project.DAL.EntityModels;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Repository
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(IUnitOfWork uow) : base(uow)
        {

        }

    }
}
