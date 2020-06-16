using AutoMapper;
using Project.Common.Application;
using Project.Common.System;
using Project.DAL.EntityModels;
using Project.Model;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class UserService : IUserService
    {
        public UserService(IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UserRepository = userRepository;
            UnitOfWork = unitOfWork;
        }

        public IMapper Mapper { get; }
        public IUserRepository UserRepository { get; }
        public IUnitOfWork UnitOfWork { get; }

        public async Task<IUserModel> GetUserNoTrackingAsync(Guid id)
        {
            var user = await UserRepository.GetAsyncNoTracking(id);

            var userModel = Mapper.Map<UserModel>(user);

            return userModel;
        }

        public async Task<IPagedList<UserModel>> FindUsersAsync(UserParameters userParameters)
        {

            var users = await UserRepository.FindUserAsync(userParameters);



            return Mapper.Map<PagedList<UserEntity>, PagedList<UserModel>>((PagedList<UserEntity>)users);
        }

        public async Task<IUserModel> AddUserAsync(IUserModel userModel)
        {
            var user = Mapper.Map<UserEntity>(userModel);

            var addedUser = await UserRepository.AddAsync(user);
            await UnitOfWork.CommitAsync();

            return Mapper.Map<IUserModel>(addedUser);
        }

        public async Task<IUserModel> UpdateUserAsync(IUserModel userModel)
        {
            var user = Mapper.Map<UserEntity>(userModel);
            var updatedUser = UserRepository.Update(user);
            await UnitOfWork.CommitAsync();
            return Mapper.Map<UserModel>(updatedUser);
        }

        public async Task<int> DeactivateUserAsync(Guid id)
        {
            await UserRepository.DeactivateAsync(id);
            return await UnitOfWork.CommitAsync();
        }

        public async Task<int> DeleteUserAsync(Guid id)
        {
            await UserRepository.Delete(id);
            return await UnitOfWork.CommitAsync();

        }





    }
}
