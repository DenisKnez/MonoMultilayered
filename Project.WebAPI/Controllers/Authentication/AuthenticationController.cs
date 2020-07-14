using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Common;
using Project.WebAPI.System;

namespace Project.WebAPI
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public DataShaper<UserRestModel> DataShaper { get; set; }

        public AuthenticationController(IMapper mapper, IUserService userService)
        {
            Mapper = mapper;
            UserService = userService;
            DataShaper = new DataShaper<UserRestModel>();
        }

        public IMapper Mapper { get; }
        public IUserService UserService { get; }

        [HttpGet("callback")]
        public IActionResult CallBack()
        {
            return Ok("CallBack");
        }

        public IActionResult Login(string username, string password)
        {
            return null;
        }

        public IActionResult Logout()
        {
            return null;
        }

        public IActionResult Register(string username, string password)
        {
            return null;
        }

        public class AuthenticationRestModel : BaseRestModel
        {
        }
    }
}