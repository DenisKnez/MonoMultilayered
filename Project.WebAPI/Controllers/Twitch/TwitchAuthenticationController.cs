using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Twitch;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Controllers.Twitch
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitchAuthenticationController : ControllerBase
    {
        public ITwitchAuthenticationService TwitchAuthenticationService { get; }

        public TwitchAuthenticationController(ITwitchAuthenticationService twitchAuthenticationService)
        {
            TwitchAuthenticationService = twitchAuthenticationService;
        }

        // GET: api/TwitchAuthentication
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/TwitchAuthentication/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TwitchAuthentication
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPost("twitch-code")]
        public async Task<IActionResult> TwitchCodeResolution([FromHeader] string code)
        {
            string result = await TwitchAuthenticationService.ExchangeCodeForTokenAsync(code);

            return Ok("");
        }

        [HttpGet("twitch-login")]
        public IActionResult TwitchLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("TwitchResponse")
            };

            return Challenge(properties, "twitch");
        }

        public async Task<IActionResult> TwitchResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            return Ok(claims);
        }

        // PUT: api/TwitchAuthentication/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}