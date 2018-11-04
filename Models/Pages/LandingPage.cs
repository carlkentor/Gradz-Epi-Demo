using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Grademoepi.Models.Pages
{
    /// <summary>
    /// Used for the site's start page and also acts as a container for site settings
    /// </summary>
    [ContentType(
        GUID = "13671657-B684-4D95-A61F-8DD4FE60D559",
        GroupName = Global.GroupNames.Specialized)]
    [SiteImageUrl]
    [AvailableContentTypes(
        Availability.Specific,
        Include = new[] { typeof(ContainerPage), typeof(StandardPage), typeof(ISearchPage), typeof(ContentFolder) }, // Pages we can create under the start page...
        ExcludeOn = new[] { typeof(ContainerPage), typeof(StandardPage), typeof(ISearchPage) })] // ...and underneath those we can't create additional start pages
    public class LandingPage : SitePageData
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 320)]
        [CultureSpecific]
        public virtual ContentArea MainContentArea { get; set; }

        [Display(GroupName = Global.GroupNames.SiteSettings)]
        public virtual PageReference SearchPageLink { get; set; }

        [AllowedTypes(new[] { typeof(SitePageData) })]
        [Display(Name = "Ingångar", GroupName = SystemTabNames.Content, Order = 1)]
        public virtual IEnumerable<ContentReference> PortalLinks { get; set; }
    }
}
