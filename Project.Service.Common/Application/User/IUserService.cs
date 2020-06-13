using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IUserService
    {
        Task<IUserModel> GetUserAsync(Guid id);

        Task AddUserAsync(IUserModel userModel);

    }
}
