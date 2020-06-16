using Project.Common.Application;
using Project.Common.System;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IUserService
    {
        Task<IUserModel> GetUserNoTrackingAsync(Guid id);

        Task<IUserModel> AddUserAsync(IUserModel userModel);

        Task<IUserModel> UpdateUserAsync(IUserModel userModel);

        Task<int> DeactivateUserAsync(Guid id);

        Task<int> DeleteUserAsync(Guid id);

        Task<PagedList<IUserModel>> FindUsersAsync(UserParameters userParameters);

    }
}
