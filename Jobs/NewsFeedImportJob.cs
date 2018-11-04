using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using Grademoepi.Models.Feeds;
using Grademoepi.Models.Pages;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;

namespace Grademoepi.Jobs
{
    [ScheduledPlugIn(DisplayName = "Fetch feeds", GUID = "e5910008-3e76-3854-b3c7-9a025a0c2603")]
    public class NewsFeedImportJob : ScheduledJobBase
    {
        private bool _stopSignaled;
        private IContentLoader contentLoader;
        private readonly IContentRepository contentRepository;
        const string apikey = "d84782292488411b87e52b5e360536dd";
        private readonly int createdCount = 0;

        private readonly HttpClient httpClient = new HttpClient();
        public NewsFeedImportJob()
        {
            IsStoppable = true;
            contentLoader = contentLoader ?? ServiceLocator.Current.GetInstance<IContentLoader>();
            contentRepository = contentRepository ?? ServiceLocator.Current.GetInstance<IContentRepository>();
        }

        /// <summary>
        /// Called when a user clicks on Stop for a manually started job, or when ASP.NET shuts down.
        /// </summary>
        public override void Stop()
        {
            _stopSignaled = true;
        }


        /// <summary>
        /// Called when a scheduled job executes
        /// </summary>
        /// <returns>A status message to be stored in the database log and visible from admin mode</returns>
        public override string Execute()
        {
            var startPage = contentLoader.Get<StartPage>(ContentReference.StartPage);
            var categoryRepository = ServiceLocator.Current.GetInstance<CategoryRepository>();
            var rootPage = contentLoader.GetChildren<ContainerPage>(ContentReference.StartPage).FirstOrDefault();
            //Call OnStatusChanged to periodically notify progress of job for manually started jobs
            OnStatusChanged(String.Format("Starting execution of {0}", this.GetType()));

            var feedCollection = startPage.NewsFeeds;
            if (feedCollection == null || !feedCollection.Any())
            {
                return $"Zero news got imported from zero feeds";
            }

            foreach (var feed in feedCollection)
            {
                var response = httpClient.GetStringAsync($"{feed}&apiKey={apikey}");
                var resultFeed = JsonConvert.DeserializeObject<Feed>(response.Result);
                var existingPages = contentLoader.GetChildren<FeedPage>(ContentReference.StartPage);
                foreach (var article in resultFeed.Articles)
                {
                    var pagename = $"{article.Source.Name}_{article.Source.Id}_{article.Title}_{article.PublishedAt}";
                    if (existingPages.Any(x => x.Name == pagename)) { continue; }
                    var feedPage = contentRepository.GetDefault<FeedPage>(rootPage.ContentLink);
                    feedPage.Name = pagename;
                    feedPage.Author = article.Author;
                    feedPage.Content = article.Content;
                    feedPage.Description = article.Description;
                    feedPage.Title = article.Title;
                    feedPage.Url = article.Url;
                    feedPage.UrlToImage = article.UrlToImage;
                    feedPage.ArticleId = article.Source.Id;

                    var sourceCat = categoryRepository.Get(article.Source.Name);
                    if (sourceCat == null)
                    {
                        var rootCat = categoryRepository.GetRoot();

                        sourceCat = new Category(rootCat, article.Source.Name)
                        {
                            Selectable = true,
                            Description = article.Source.Name
                        };

                        categoryRepository.Save(sourceCat);
                    }
                    feedPage.Category.Add(sourceCat.ID);

                    contentRepository.Save(feedPage, EPiServer.DataAccess.SaveAction.Publish, EPiServer.Security.AccessLevel.NoAccess);
                }
            }

            //For long running jobs periodically check if stop is signaled and if so stop execution
            if (_stopSignaled)
            {
                return "Stop of job was called";
            }

            return $"Imported {createdCount} articles from {feedCollection.Count} feeds";
        }
    }
}