using Grademoepi.Models.Blocks;
using Grademoepi.Models.Pages;
using EPiServer.Cms.TinyMce.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace Grademoepi.Business.Initialization
{
    [ModuleDependency(typeof(TinyMceInitialization))]
    public class ExtendedTinyMceInitialization : IConfigurableModule
    {
        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.Configure<TinyMceConfiguration>(config =>
            {
                // Add content CSS to the default settings.
                config.Default()
                    .ContentCss("/static/css/editor.css");

                // Passing a second argument to For<> will clone the given settings object
                // instead of the default one and extend it with some basic toolbar commands.
                config.For<EditorialBlock>(t => t.MainBody, config.Empty())
                    .AddEpiserverSupport()
                    .DisableMenubar()
                    .Toolbar("bold italic underline strikethrough");
            });
        }
    }
}
