using System.Collections.Generic;

namespace Grademoepi.Models.Feeds
{
    public class Feed
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }

        public List<FeedItem> Articles { get; set; }
    }
}