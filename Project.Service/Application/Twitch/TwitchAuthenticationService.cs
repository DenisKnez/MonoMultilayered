using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Project.Service.Twitch
{
    public class TwitchAuthenticationService : ITwitchAuthenticationService
    {
        public TwitchAuthenticationService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            Configuration = configuration;
        }

        public HttpClient httpClient { get; }
        public IConfiguration Configuration { get; }

        public async Task<string> ExchangeCodeForTokenAsync(string code)
        {
            string clientId = Configuration.GetSection("TwitchAuth")["ClientId"];
            string clientSecret = Configuration.GetSection("TwitchAuth")["ClientSecret"];
            string redirectUri = Configuration.GetSection("TwitchAuth")["RedirectUri"];

            string url = $"https://id.twitch.tv/oauth2/token?client_id={clientId}&client_secret={clientSecret}&code={code}&grant_type=authorization_code&redirect_uri={redirectUri}";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

            //httpRequest.Content.Headers.Remove("Content-Type");
            httpRequest.Headers.Add("Content-Type", "application/json");

            var result = await httpClient.SendAsync(httpRequest);

            Console.WriteLine("Response: " + result);

            return "";
        }
    }
}