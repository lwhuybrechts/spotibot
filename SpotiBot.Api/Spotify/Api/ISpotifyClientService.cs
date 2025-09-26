using SpotiBot.Api.Spotify.Playlists;
using SpotiBot.Api.Spotify.Tracks;
using SpotifyAPI.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotiBot.Api.Spotify.Api
{
    public interface ISpotifyClientService
    {
        Task<Track> GetTrack(ISpotifyClient spotifyClient, string trackId);
        Task<Playlist> GetPlaylist(ISpotifyClient spotifyClient, string playlistId);
        Task<List<Track>> GetTracks(ISpotifyClient spotifyClient, string playlistId);
        Task AddTrackToPlaylist(ISpotifyClient spotifyClient, Track track);
        Task RemoveTrackFromPlaylist(ISpotifyClient spotifyClient, string trackId, string playlistId);
        Task<bool> AddToQueue(ISpotifyClient spotifyClient, Track track);
    }
}
