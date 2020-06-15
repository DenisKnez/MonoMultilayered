using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Project.WebAPI.System;
using Project.Common.System;
using Project.Model;

namespace Project.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IMapper mapper, IUserService userService)
        {
            Mapper = mapper;
            UserService = userService;
        }

        public IMapper Mapper { get; }
        public IUserService UserService { get; }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(Guid id)
        {
            var user = await UserService.GetUserAsync(id);

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


        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]PagingParameters pagingParameters, [FromQuery]SortingParameters sortingParameters)
        {

            return null;
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



    }

}