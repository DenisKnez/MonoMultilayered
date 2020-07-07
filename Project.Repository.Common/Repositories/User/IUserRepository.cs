using Project.Common;
using Project.Common.Filters;
using Project.Common.System;
using Project.DAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IPagedList<User>> FindUserAsync(IParameters<IUserFilter> userParameters);
    }
}
