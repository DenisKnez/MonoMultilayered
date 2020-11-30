using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Project.Service.Application.Twitch.Utility;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Project.Service.Twitch
{
    public class TwitchAuthenticationService : ITwitchAuthenticationService
    {
        public TwitchAuthenticationService(HttpClient httpClient, IConfiguration configuration, TwitchToken twitchToken)
        {
            this.httpClient = httpClient;
            Configuration = configuration;
            Token = twitchToken;
        }

        public HttpClient httpClient { get; }
        public IConfiguration Configuration { get; }
        public TwitchToken Token { get; set; }

        public async Task<string> ExchangeCodeForTokenAsync(string code)
        {
            Console.WriteLine("code: " + code);
            string clientId = Configuration.GetSection("TwitchAuth")["ClientId"];
            string clientSecret = Configuration.GetSection("TwitchAuth")["ClientSecret"];
            string redirectUri = Configuration.GetSection("TwitchAuth")["RedirectUri"];

            string url = $"https://id.twitch.tv/oauth2/token?client_id={clientId}&client_secret={clientSecret}&code={code}&grant_type=authorization_code&redirect_uri={redirectUri}";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            httpClient.DefaultRequestHeaders.Add("client-id", clientId);

            var result = await httpClient.SendAsync(httpRequest);

            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                Token = JsonConvert.DeserializeObject<TwitchToken>(content);
            }

            Console.WriteLine("Response: " + Token.access_token);

            return "";
        }
    }
}