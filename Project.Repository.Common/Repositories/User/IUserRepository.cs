using Project.Common.Application;
using Project.Common.System;
using Project.DAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<IPagedList<UserEntity>> FindUserAsync(UserParameters parameters);
    }
}
