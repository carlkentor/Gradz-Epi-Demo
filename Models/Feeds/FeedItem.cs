using System;
using System.Collections.Generic;

namespace Grademoepi.Models.Feeds
{
    public class FeedItem
    {
        public FeedSource Source { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }

        public List<string> EpiCategories {get;set;}
    }
}