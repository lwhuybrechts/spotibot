using static SpotiBot.Api.Bot.Votes.VoteAttributes;

namespace SpotiBot.Api.Bot.Votes
{
    public enum VoteType
    {
        Upvote,
        [UseNegativeOperator]
        Downvote
    }
}
