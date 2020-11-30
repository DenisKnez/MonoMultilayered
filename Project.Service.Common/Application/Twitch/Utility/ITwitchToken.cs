namespace Project.Service.Common.Application.Twitch.Utility
{
    public interface ITwitchToken
    {
        string access_token { get; set; }

        string refresh_token { get; set; }

        int expires_in { get; set; }

        string[] scope { get; set; }

        string token_type { get; set; }
    }
}