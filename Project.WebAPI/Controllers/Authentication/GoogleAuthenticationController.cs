using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Google;

namespace Project.WebAPI.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleAuthenticationController : ControllerBase
    {

        // GET api/GoogleAuthenticationController
        [HttpGet("login")]
        public IActionResult GoogleLogin()
        {
            AuthenticationProperties options = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };


            return Challenge(options);
        }

        // GET api/GoogleAuthenticationController
        [HttpGet("spa")]
        public IActionResult GoogleSpa()
        {
            

            AuthenticationProperties options = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };


            return Challenge(options);
        }


        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Ok("The authentication failed");
        }
    }
}
