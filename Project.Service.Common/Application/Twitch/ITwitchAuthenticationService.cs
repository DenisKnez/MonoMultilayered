using System.Threading.Tasks;

namespace Project.Service.Twitch
{
    public interface ITwitchAuthenticationService
    {
        Task<string> ExchangeCodeForTokenAsync(string code);
    }
}