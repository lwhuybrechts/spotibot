using Azure.Data.Tables;
using SpotiBot.Api.Library;

namespace SpotiBot.Api.Bot.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TableServiceClient tableServiceClient)
            : base(tableServiceClient.GetTableClient(typeof(User).Name), "users")
        {

        }
    }
}
