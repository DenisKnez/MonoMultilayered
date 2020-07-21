using Project.Common;
using Project.Common.Filters;
using Project.Common.System;
using Project.DAL.EntityModels;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IPagedList<User>> FindUserAsync(Parameters<UserFilter> userIParameters);
    }
}