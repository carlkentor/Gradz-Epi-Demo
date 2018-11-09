using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Grademoepi.Models.Feeds
{
    [HubName("feedHub")]
    public class FeedHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.newFeedArticle(name, message);
        }
    }
}