using AutoMapper;
using Project.Model;
using Project.Repository.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class UserService : IUserService
    {
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            Mapper = mapper;
            UserRepository = userRepository;
        }

        public IMapper Mapper { get; }
        public IUserRepository UserRepository { get; }

        public async Task<UserModel> GetUserAsync(Guid id)
        {
            var user = await UserRepository.GetAsync(id);

            return Mapper.Map<UserModel>(user);
        }



    }
}
