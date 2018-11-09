using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Grademoepi.Models.Feeds;
using System;

namespace Grademoepi.Models.Pages
{
    [ContentType(DisplayName = "FeedPage", GUID = "ca334c61-9a7c-4687-907d-59d27edecb26", Description = "", AvailableInEditMode = false)]
    public class FeedPage : SitePageData
    {

        public virtual string Author { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Url { get; set; }
        public virtual string UrlToImage { get; set; }
        public virtual DateTime PublishedAt { get; set; }
        public virtual string Content { get; set; }

        public virtual string ArticleId {get;set;}
    }
}