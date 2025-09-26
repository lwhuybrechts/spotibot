using SpotifyAPI.Web;

namespace SpotiBot.Api.Spotify.Authorization
{
    public interface IMapper
    {
        AuthorizationCodeTokenResponse Map(AuthorizationToken source);
        AuthorizationToken Map(AuthorizationCodeTokenResponse source);
    }
}
