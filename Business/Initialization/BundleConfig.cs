using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using System.Linq;
using System.Web.Optimization;

namespace Grademoepi.Business.Initialization
{
    [InitializableModule]
    public class BundleConfig : IInitializableModule
    {
        Injected<IContentTypeRepository> contentTypeRepo;
        public void Initialize(InitializationEngine context)
        {
            if (context.HostType == HostType.WebApplication)
            {
                RegisterBundles(BundleTable.Bundles);
                HookPageTypes(BundleTable.Bundles);
            }
        }

        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Static/js/bootstrap.js",
                        "~/Static/js/site.js"));

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Static/css/bootstrap.css", new CssRewriteUrlTransform())
                         .Include("~/Static/css/site.css", new CssRewriteUrlTransform())
                            .Include("~/Static/open-iconic-master/font/css/open-iconic.css")
                .Include("~/Static/open-iconic-master/font/css/open-iconic-bootstrap.css")
                .Include("~/Static/css/editmode.css")
                .Include("~/Static/css/gradz-animations.css"));
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }

        public void HookPageTypes(BundleCollection bundles)
        {
            var allPageTypes = contentTypeRepo.Service.List().Where(c => typeof(PageData).IsAssignableFrom(c.ModelType));

            foreach (var pagetype in allPageTypes)
            {
                bundles.Add(new ScriptBundle($"~/bundles/{pagetype.Name}").Include($"~/Static/css/{pagetype.Name.ToLower()}.css"));
            }

        }
    }
}
