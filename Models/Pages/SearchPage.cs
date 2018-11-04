using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Grademoepi.Models.Blocks;
using System.ComponentModel.DataAnnotations;

namespace Grademoepi.Models.Pages
{
    /// <summary>
    /// Used to provide on-site search
    /// </summary>
    [SiteContentType(
        GUID = "AAC25733-1D21-4F82-B031-11E626C91E30",
        GroupName = Global.GroupNames.Specialized)]
    [SiteImageUrl]
    public class SearchPage : SitePageData, IHasRelatedContent, ISearchPage
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 310)]
        [CultureSpecific]
        [AllowedTypes(new[] { typeof(IContentData) }, new[] { typeof(JumbotronBlock) })]
        public virtual ContentArea RelatedContentArea { get; set; }


        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            VisibleInMenu = false;
        }
    }
}
