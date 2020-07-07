using Project.Common;
using Project.Common.Filters;
using Project.Common.System;
using Project.Model;
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

        Task<IPagedList<UserModel>> FindUsersAsync(IParameters<IUserFilter> userParameters);

    }
}
