using EPiServer.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Grademoepi
{

    public class Global
    {
        public static readonly string LoginPath = "/util/login.aspx";
        public static readonly string AppRelativeLoginPath = string.Format("~{0}", LoginPath);

        /// <summary>
        /// Group names for content types and properties
        /// </summary>
        [GroupDefinitions()]
        public static class GroupNames
        {
            [Display(Name = "Contact", Order = 9000)]
            public const string Contact = "Contact";

            [Display(Name = "Default", Order = 9100)]
            public const string Default = "Default";

            [Display(Name = "Metadata", Order = 9200)]
            public const string MetaData = "Metadata";

            [Display(Name = "News", Order = 9300)]
            public const string News = "News";

            [Display(Name = "Products", Order = 9400)]
            public const string Products = "Products";

            [Display(Name = "SiteSettings", Order = 9500)]
            public const string SiteSettings = "SiteSettings";

            [Display(Name = "Specialized", Order = 9600)]
            public const string Specialized = "Specialized";

            [Display(Name = "Feeds", Order = 9700)]
            public const string Feeds = "Feeds";
        }

        /// <summary>
        /// Tags to use for the main widths used in the Bootstrap HTML framework
        /// </summary>
        public static class ContentAreaTags
        {
            public const string FullWidth = "col-md-12";
            public const string TwoThirdsWidth = "col-md-8";
            public const string HalfWidth = "col-md-6";
            public const string OneThirdWidth = "col-md-4";
            public const string NoRenderer = "norenderer";
            public const string PortalReference = "PortalReference";
        }

        /// <summary>
        /// Main widths used in the Bootstrap HTML framework
        /// </summary>
        public static class ContentAreaWidths
        {
            public const int FullWidth = 12;
            public const int TwoThirdsWidth = 8;
            public const int HalfWidth = 6;
            public const int OneThirdWidth = 4;
        }

        public static Dictionary<string, int> ContentAreaTagWidths = new Dictionary<string, int>
            {
                { ContentAreaTags.FullWidth, ContentAreaWidths.FullWidth },
                { ContentAreaTags.TwoThirdsWidth, ContentAreaWidths.TwoThirdsWidth },
                { ContentAreaTags.HalfWidth, ContentAreaWidths.HalfWidth },
                { ContentAreaTags.OneThirdWidth, ContentAreaWidths.OneThirdWidth }
            };

        /// <summary>
        /// Names used for UIHint attributes to map specific rendering controls to page properties
        /// </summary>
        public static class SiteUIHints
        {
            public const string Contact = "contact";
            public const string Strings = "StringList";
            public const string StringsCollection = "StringsCollection";
        }

        /// <summary>
        /// Virtual path to folder with static graphics, such as "~/Static/gfx/"
        /// </summary>
        public const string StaticGraphicsFolderPath = "~/Static/gfx/";
    }
}
