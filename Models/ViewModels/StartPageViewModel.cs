using Grademoepi.Models.Feeds;
using Grademoepi.Models.Pages;
using System.Collections.Generic;

namespace Grademoepi.Models.ViewModels
{
    public class StartPageViewModel : PageViewModel<StartPage>
    {
        public StartPageViewModel(StartPage currentPage) : base(currentPage)
        {

        }
        public List<FeedItem> FeedItems { get; set; }
    }
}