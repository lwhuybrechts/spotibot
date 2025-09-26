using System.Threading.Tasks;

namespace SpotiBot.Api.Spotify.Authorization
{
    public interface IAuthorizationTokenRepository
    {
        Task<AuthorizationToken> Get(long rowKey, string partitionKey = "");
        Task Upsert(AuthorizationToken item);
    }
}
