using SpotiBot.Api.Bot.HandleUpdate.Dto;
using SpotiBot.Api.Bot.Users;
using SpotiBot.Api.Spotify.Tracks;

namespace SpotiBot.Api.Spotify.Tracks.AddTrack
{
    public interface IReplyMessageService
    {
        string GetSuccessReplyMessage(UpdateDto updateDto, Track track);
        string GetExistingTrackReplyMessage(UpdateDto updateDto, Track track, User user);
    }
}
