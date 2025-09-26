using Azure.Data.Tables;
using SpotiBot.Api.Library;

namespace SpotiBot.Api.Bot.Chats
{
    public class ChatMemberRepository : BaseRepository<ChatMember>, IChatMemberRepository
    {
        public ChatMemberRepository(TableServiceClient tableServiceClient)
            : base(tableServiceClient.GetTableClient(typeof(ChatMember).Name), null)
        {

        }
    }
}
