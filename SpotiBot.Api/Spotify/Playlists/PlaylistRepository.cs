using Azure.Data.Tables;
using SpotiBot.Api.Library;

namespace SpotiBot.Api.Spotify.Playlists
{
    public class PlaylistRepository : BaseRepository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(TableServiceClient tableServiceClient)
            : base(tableServiceClient.GetTableClient(typeof(Playlist).Name), "playlists")
        {

        }
    }
}
