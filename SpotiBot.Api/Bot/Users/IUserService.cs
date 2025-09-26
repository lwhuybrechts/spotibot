using System.Threading.Tasks;

namespace SpotiBot.Api.Bot.Users
{
    public interface IUserService
    {
        Task<User> SaveUser(User user, long chatId);
    }
}