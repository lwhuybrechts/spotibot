using System.Threading.Tasks;

namespace SpotiBot.Api.Spotify.Tracks.SyncTracks
{
    public interface ISyncTracksService
    {
        Task SyncTracks(Bot.Chats.Chat chat, bool shouldUpdateExistingTracks = false);
    }
}