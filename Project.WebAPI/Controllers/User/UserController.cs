using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Common;
using Project.Common.Filters;
using Project.Common.System;
using Project.Model;
using Project.Service.Common;
using Project.WebAPI.System;
using System;
using System.Threading.Tasks;

namespace Project.WebAPI
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public DataShaper<UserRestModel> DataShaper { get; set; }

        public UserController(IMapper mapper, IUserService userService)
        {
            Mapper = mapper;
            UserService = userService;
            DataShaper = new DataShaper<UserRestModel>();
        }

        public IMapper Mapper { get; }
        public IUserService UserService { get; }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(Guid id, string fields = "")
        {
            var user = await UserService.GetUserNoTrackingAsync(id);

            if (user != null)
            {
                UserRestModel restUser = Mapper.Map<UserRestModel>(user);

                return Ok(DataShaper.ShapeData(restUser, fields));
            }
            else
            {
                return NotFound("The user was not found");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] Parameters<UserFilter> userIParameters, string fields = "")
        {
            var users = await UserService.FindUsersAsync(userIParameters);

            var restUsers = Mapper.Map<PagedList<UserRestModel>>(users);

            return Ok(DataShaper.ShapeData(restUsers, fields));
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody]UserRestModel userRestModel)
        {
            var user = Mapper.Map<UserModel>(userRestModel);

            await UserService.AddUserAsync(user);

            if (user != null)
            {
                var restUser = Mapper.Map<UserRestModel>(user);
                return Ok(restUser);
            }
            else
            {
                return NotFound("The user was not found");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]UserRestModel userRestModel)
        {
            var user = await UserService.GetUserNoTrackingAsync(userRestModel.Id);

            var userModel = Mapper.Map<UserModel>(userRestModel);

            var anotherUser = Mapper.Map(userModel, user);

            await UserService.UpdateUserAsync(anotherUser);

            if (user != null)
            {
                var restUser = Mapper.Map<UserRestModel>(user);
                return Ok(restUser);
            }
            else
            {
                return NotFound("The user was not found");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeactivateUser(Guid id)
        {
            int numberOfChanges = await UserService.DeactivateUserAsync(id);

            if (numberOfChanges > 1)
            {
                return Ok("The user was deactivated");
            }
            else
            {
                return NotFound("The user was not found");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task DeleteUser(Guid id)
        {
            await UserService.DeleteUserAsync(id);
        }
    }

    public class UserRestModel : BaseRestModel
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public decimal? Salary { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime? DateJoined { get; set; }

        public Guid CompanyId { get; set; }

        public CompanyRestModel Company { get; set; }
    }
}