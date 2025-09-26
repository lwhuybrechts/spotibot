using SpotifyAPI.Web;

namespace SpotiBot.Api.Spotify.Playlists
{
    public interface IMapper
    {
        Playlist Map(FullPlaylist source);
    }
}
