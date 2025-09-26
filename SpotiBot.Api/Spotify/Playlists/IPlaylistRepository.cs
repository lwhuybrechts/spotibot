using System.Threading.Tasks;

namespace SpotiBot.Api.Spotify.Playlists
{
    public interface IPlaylistRepository
    {
        Task<Playlist> Get(string rowKey, string partitionKey = "");
        Task<Playlist> Get(Playlist item);
        Task Upsert(Playlist item);
        Task Delete(Playlist item);
    }
}