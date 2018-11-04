using EPiServer;
using EPiServer.Cms.Shell;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Grademoepi.Models.Pages;

namespace Grademoepi.Business.Initialization
{

    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class FeedEventInitialization : IInitializableModule
    {
        private IContentEvents _contentEvents;
        private IContentRepository _contentRepository;
        public void Initialize(InitializationEngine context)
        {
            _contentRepository = (_contentRepository ?? ServiceLocator.Current.GetInstance<IContentRepository>());
            _contentEvents = _contentEvents ?? ServiceLocator.Current.GetInstance<IContentEvents>();
            _contentEvents.CreatedContent += CreatedContent;
        }

        public void CreatedContent(object sender, ContentEventArgs e)
        {
            var sitepagedata = e.Content as FeedPage;
            if (sitepagedata == null)
            {
                return;
            }
        }

        public void Preload(string[] parameters) { }

        public void Uninitialize(InitializationEngine context)
        {
            _contentEvents = _contentEvents ?? ServiceLocator.Current.GetInstance<IContentEvents>();
            _contentEvents.PublishingContent -= CreatedContent;
        }
    }
}