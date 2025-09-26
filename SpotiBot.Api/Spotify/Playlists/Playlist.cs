using SpotiBot.Api.Library;
using System.Runtime.Serialization;

namespace SpotiBot.Api.Spotify.Playlists
{
    public class Playlist : MyTableEntity
    {
        [IgnoreDataMember]
        public string Id
        {
            get { return RowKey; }
            set { RowKey = value; }
        }

        public string Name { get; set; }
    }
}
