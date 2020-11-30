using Microsoft.AspNetCore.Mvc;
using Project.Service.Twitch;

namespace Project.WebAPI.Controllers.Twitch
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitchController : ControllerBase
    {
        public ITwitchService TwitchService { get; }

        public TwitchController(ITwitchService twitchService)
        {
            TwitchService = twitchService;
        }

        // GET: api/Twitch/users/current
        [HttpGet("users/current")]
        public IActionResult CurrentUser()
        {
            string user = TwitchService.GetUserInfo();

            if (user == null)
            {
                return new StatusCodeResult(500);
            }

            return Ok(user);
        }
    }
}