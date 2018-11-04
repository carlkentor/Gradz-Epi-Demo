using EPiServer;
using EPiServer.Cms.Shell;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Grademoepi.Models.Pages;
using System;
using System.Linq;
using System.Reflection;

namespace Grademoepi.Business.Initialization
{

    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class LoremIpsumContentInitialization : IInitializableModule
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
            var sitepagedata = e.Content as SitePageData;
            if (sitepagedata == null)
            {
                return;
            }
            var clone = sitepagedata.CreateWritableClone() as SitePageData;
            var noninheritedProperties = sitepagedata.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(x => x.PropertyType == typeof(XhtmlString) || x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(string)).ToList();
            var enumerator = noninheritedProperties.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                var prop = sitepagedata.Property.FirstOrDefault(x => x.Name == current.Name);
                if (prop == null) { continue; }
                object propvalue;
                switch (prop.Type)
                {
                    case PropertyDataType.String:
                        {
                            propvalue = LoremNET.Lorem.Words(new Random().Next(3, 5));
                            break;
                        }
                    case PropertyDataType.LongString:
                        {
                            if (current.PropertyType == typeof(XhtmlString))
                            {
                                propvalue = new XhtmlString(LoremNET.Lorem.Sentence(new Random().Next(200, 500)));
                                break;
                            }
                            continue;
                        }
                    case PropertyDataType.Date:
                        {
                            propvalue = LoremNET.Lorem.DateTime(new DateTime(1995, 1, 1), new DateTime(2020, 12, 31));
                            break;
                        }
                    default:
                        {
                            continue;
                        }
                }

                clone.SetPropertyValue(current.Name, propvalue);
            }
            _contentRepository.Save(clone, EPiServer.DataAccess.SaveAction.CheckIn, EPiServer.Security.AccessLevel.NoAccess);
        }

        public void Preload(string[] parameters) { }

        public void Uninitialize(InitializationEngine context)
        {
            _contentEvents = _contentEvents ?? ServiceLocator.Current.GetInstance<IContentEvents>();
            _contentEvents.PublishingContent -= CreatedContent;
        }
    }
}