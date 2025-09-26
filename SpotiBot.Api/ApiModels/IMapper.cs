using SpotiBot.Api.Bot.Votes;
using SpotiBot.Api.Spotify.Authorization;
using SpotiBot.ApiModels;
using System.Collections.Generic;

namespace SpotiBot.Api.ApiModels
{
    public interface IMapper
    {
        Track Map(Spotify.Tracks.Track source);
        List<Track> Map(List<Spotify.Tracks.Track> source);
        Upvote Map(Vote source);
        List<Upvote> Map(List<Vote> source);
        User Map(Bot.Users.User source);
        List<User> Map(List<Bot.Users.User> source);
        SpotifyAccessToken Map(AuthorizationToken source);
    }
}
