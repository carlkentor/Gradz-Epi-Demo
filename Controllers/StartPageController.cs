using EPiServer;
using EPiServer.DataAbstraction;
using Grademoepi.Models.Pages;
using Grademoepi.Models.ViewModels;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
namespace Grademoepi.Controllers
{
    public class StartPageController : PageControllerBase<StartPage>
    {
        private readonly IContentLoader contentLoader;
        private readonly CategoryRepository categoryRepository;
        public StartPageController(IContentLoader contentLoader, CategoryRepository categoryRepository)
        {
            this.contentLoader = contentLoader;
            this.categoryRepository = categoryRepository;
        }
        public ActionResult Index(StartPage currentPage)
        {
            var newsroot = contentLoader.Get<ContainerPage>(currentPage.NewsRoot, CultureInfo.GetCultureInfo("en"));
            var model = new StartPageViewModel(currentPage)
            {
                FeedItems = contentLoader.GetChildren<FeedPage>(newsroot.ContentLink, CultureInfo.GetCultureInfo("en")).Select(x => new Models.Feeds.FeedItem()
                {
                    EpiCategories = x.Category != null ? x.Category.Select(c => categoryRepository.Get(c).Name).ToList() : new List<string>(),
                    Author = x.Author,
                    Content = x.Content,
                    Description = x.Description,
                    PublishedAt = x.PublishedAt,
                    Title = x.Title,
                    Url = x.Url,
                    UrlToImage = x.UrlToImage
                }).ToList()
            };
            return View(model);
        }

    }
}
