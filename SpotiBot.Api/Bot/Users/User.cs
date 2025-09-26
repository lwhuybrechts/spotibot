using SpotiBot.Api.Library;
using System.Runtime.Serialization;

namespace SpotiBot.Api.Bot.Users
{
    public class User : MyTableEntity
    {
        [IgnoreDataMember]
        public long Id
        {
            get { return long.Parse(RowKey); }
            set { RowKey = value.ToString(); }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [IgnoreDataMember]
        public string? LanguageCode { get; set; }
    }
}
