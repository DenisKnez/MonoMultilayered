using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        public UserController(IMapper mapper, IUserService userService)
        {
            Mapper = mapper;
            UserService = userService;
        }

        public IMapper Mapper { get; }
        public IUserService UserService { get; }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            //UserService

            return null;
        }



    }
}
