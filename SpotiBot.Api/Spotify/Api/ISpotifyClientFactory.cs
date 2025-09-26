using SpotifyAPI.Web;
using System.Threading.Tasks;

namespace SpotiBot.Api.Spotify.Api
{
    public interface ISpotifyClientFactory
    {
        Task<ISpotifyClient> Create(long userId);
    }
}