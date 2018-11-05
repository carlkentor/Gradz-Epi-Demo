using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Grademoepi.Business.Rendering;

namespace Grademoepi.Models.Pages
{
    /// <summary>
    /// Used to logically group pages in the page tree
    /// </summary>
    [SiteContentType(
        GUID = "D178950C-D20E-4A46-90BD-5338B2424745",
        GroupName = Global.GroupNames.Specialized)]
    [SiteImageUrl]
    [AvailableContentTypes(Availability.Specific, Include = new[] { typeof(SitePageData) })]// Pages we can create under the start page...
    public class ContainerPage : SitePageData, IContainerPage
    {

    }
}
