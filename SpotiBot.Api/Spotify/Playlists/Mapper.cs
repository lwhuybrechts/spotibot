using SpotifyAPI.Web;

namespace SpotiBot.Api.Spotify.Playlists
{
    public class Mapper : IMapper
    {
        public Playlist Map(FullPlaylist source) => new()
        {
            Id = source.Id,
            Name = source.Name
        };
    }
}
