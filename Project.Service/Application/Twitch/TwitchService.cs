using Microsoft.Extensions.Configuration;
using Project.Service.Application.Twitch.Utility;
using System.Net.Http;
using System.Security.Claims;

namespace Project.Service.Twitch
{
    public class TwitchService : ITwitchService
    {
        public TwitchService(HttpClient httpClient, ClaimsPrincipal user, IConfiguration configuration, TwitchToken twitchToken)
        {
            this.httpClient = httpClient;
            Configuration = configuration;
            Token = twitchToken;
            User = user;
        }

        public ClaimsPrincipal User { get; set; }
        public HttpClient httpClient { get; }
        public IConfiguration Configuration { get; }
        public TwitchToken Token { get; set; }

        public string GetUserInfo()
        {
            //string clientId = Configuration.GetSection("TwitchAuth")["ClientId"];

            //string url = "";

            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            //httpClient.DefaultRequestHeaders.Add("client-id", clientId);

            //var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
            //var result = await httpClient.SendAsync(httpRequest);

            return User.Identity.Name;
        }
    }
}