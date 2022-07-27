using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace $ucprojectname$.Areas.Admin.Models.$ucprojectname$
{
    /// <summary>
    /// Represents a $lcent$ search model
    /// </summary>
    public partial record $Entity$SearchModel : BaseSearchModel
    {
        public const string LIST_VIEW = "~/Plugins/Widgets.$ucprojectname$/Web/Areas/Admin/Views/$Entity$/List.cshtml";
        public const string SYSTEM_NAME = "Widgets.$ucprojectname$.$Entity$ListMenuItem";
        #region Labels
        public static class Labels
        {

            public const string Search$Entity$Name = "Admin.$ucprojectname$.$Entity$s.List.Search$Entity$Name";
            public const string SearchStoreId = "Admin.$ucprojectname$.$Entity$s.List.SearchStore";
            public const string ImportFromExcelTip = "Admin.$ucprojectname$.$Entity$s.List.ImportFromExcelTip";

            public const string SearchPublishedId = "Admin.$ucprojectname$.$Entity$s.List.SearchPublished";
            public const string All = "Admin.$ucprojectname$.$Entity$s.List.SearchPublished.All";
            public const string PublishedOnly = "Admin.$ucprojectname$.$Entity$s.List.SearchPublished.PublishedOnly";
            public const string UnpublishedOnly = "Admin.$ucprojectname$.$Entity$s.List.SearchPublished.UnpublishedOnly";

            public const string DownloadPDF = "Admin.$ucprojectname$.$Entity$s.List.DownloadPDF";

        }

        #endregion
        #region Ctor

        public $Entity$SearchModel()
        {
            AvailableStores = new List<SelectListItem>();
            AvailablePublishedOptions = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName(Labels.Search$Entity$Name)]
        public string Search$Entity$Name { get; set; }

        [NopResourceDisplayName(Labels.SearchPublishedId)]
        public int SearchPublishedId { get; set; }

        public IList<SelectListItem> AvailablePublishedOptions { get; set; }

        [NopResourceDisplayName(Labels.SearchStoreId)]
        public int SearchStoreId { get; set; }

        public IList<SelectListItem> AvailableStores { get; set; }

        public bool HideStoresList { get; set; }

        #endregion
    }
}