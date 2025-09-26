using Azure.Data.Tables;
using SpotiBot.Api.Library;

namespace SpotiBot.Api.Spotify.Authorization
{
    public class AuthorizationTokenRepository : BaseRepository<AuthorizationToken>, IAuthorizationTokenRepository
    {
        public AuthorizationTokenRepository(TableServiceClient tableServiceClient)
            : base(tableServiceClient.GetTableClient(typeof(AuthorizationToken).Name), "authorizationtokens")
        {
        }
    }
}
