using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Areas.Admin.Models.Settings;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace $ucprojectname$.Areas.Admin.Models.Settings
{
    /// <summary>
    /// Represents a hr settings model
    /// </summary>
    public partial record $Entity$SettingsModel : BaseNopModel, ISettingsModel
    {
        /// <summary>
        /// System name used on menu item and activation of menu item.
        /// </summary>
        public const string SYSTEM_NAME = "Human Resource settings";
        #region Models View Path
        public const string View = "~/Plugins/Widgets.$ucprojectname$/Web/Areas/Admin/Views/Setting/$Entity$.cshtml";
        #endregion

        #region Labels
        public static class Labels
        {
            public const string Title = "Admin.Configuration.Settings.$ucprojectname$";

            public const string EditSettings = "ActivityLog.EditSettings";
            public const string Updated = "Admin.Configuration.Updated";
            public const string Grid = "Admin.$ucprojectname$.ViewMode.Grid";
            public const string List = "Admin.$ucprojectname$.ViewMode.List";

            public const string DefaultViewMode = "Admin.Configuration.Settings.$ucprojectname$.DefaultViewMode";
            public const string ShowShareButton = "Admin.Configuration.Settings.$ucprojectname$.ShowShareButton";
            public const string PageShareCode = "Admin.Configuration.Settings.$ucprojectname$.PageShareCode";
            public const string EmailAFriendEnabled = "Admin.Configuration.Settings.$ucprojectname$.EmailAFriendEnabled";
            public const string AllowAnonymousUsersToEmailAFriend = "Admin.Configuration.Settings.$ucprojectname$.AllowAnonymousUsersToEmailAFriend";
            public const string SearchPageAllowCustomersToSelectPageSize = "Admin.Configuration.Settings.$ucprojectname$.SearchPageAllowCustomersToSelectPageSize";
            public const string SearchPagePageSizeOptions = "Admin.Configuration.Settings.$ucprojectname$.SearchPagePageSizeOptions";
            public const string ExportImport$Entity$sUsing$Entity$Name = "Admin.Configuration.Settings.$ucprojectname$.ExportImport$Entity$sUsing$Entity$Name";
            public const string ExportImportAllowDownloadImages = "Admin.Configuration.Settings.$ucprojectname$.ExportImportAllowDownloadImages";
            public const string ExportImportRelatedEntitiesByName = "Admin.Configuration.Settings.$ucprojectname$.ExportImportRelatedEntitiesByName";
            public const string $Entity$BreadcrumbEnabled = "Admin.Configuration.Settings.$ucprojectname$.$Entity$BreadcrumbEnabled";
            public const string IgnoreAcl = "Admin.Configuration.Settings.$ucprojectname$.IgnoreAcl";
            public const string IgnoreStoreLimitations = "Admin.Configuration.Settings.$ucprojectname$.IgnoreStoreLimitations";
            public const string DisplayDatePreOrderAvailability = "Admin.Configuration.Settings.$ucprojectname$.DisplayDatePreOrderAvailability";
            public const string EnableSpecificationAttributeFiltering = "Admin.Configuration.Settings.$ucprojectname$.EnableSpecificationAttributeFiltering";
            public const string DisplayAllPicturesOn$ucprojectname$Pages = "Admin.Configuration.Settings.$ucprojectname$.DisplayAllPicturesOn$ucprojectname$Pages";
            public const string AllowCustomersToSearchWith$Entity$Name = "Admin.Configuration.Settings.$ucprojectname$.AllowCustomersToSearchWith$Entity$Name";

            public const string Search = "Admin.Configuration.Settings.$ucprojectname$.BlockTitle.Search";
            public const string Performance = "Admin.Configuration.Settings.$ucprojectname$.BlockTitle.Performance";
            public const string Share = "Admin.Configuration.Settings.$ucprojectname$.BlockTitle.Share";
            public const string AdditionalSections = "Admin.Configuration.Settings.$ucprojectname$.BlockTitle.AdditionalSections";
            public const string $ucprojectname$Pages = "Admin.Configuration.Settings.$ucprojectname$.BlockTitle.$ucprojectname$Pages";
            public const string ExportImport = "Admin.Configuration.Settings.$ucprojectname$.BlockTitle.ExportImport";
        }
        #endregion

        #region Ctor

        public $Entity$SettingsModel()
        {
            AvailableViewModes = new List<SelectListItem>();
            SortOptionSearchModel = new SortOptionSearchModel();
        }

        #endregion

        #region Properties

        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName(Labels.DefaultViewMode)]
        public string DefaultViewMode { get; set; }
        public bool DefaultViewMode_OverrideForStore { get; set; }
        public IList<SelectListItem> AvailableViewModes { get; set; }

        [NopResourceDisplayName(Labels.ShowShareButton)]
        public bool ShowShareButton { get; set; }
        public bool ShowShareButton_OverrideForStore { get; set; }

        [NopResourceDisplayName(Labels.PageShareCode)]
        public string PageShareCode { get; set; }
        public bool PageShareCode_OverrideForStore { get; set; }

        [NopResourceDisplayName(Labels.EmailAFriendEnabled)]
        public bool EmailAFriendEnabled { get; set; }
        public bool EmailAFriendEnabled_OverrideForStore { get; set; }

        [NopResourceDisplayName(Labels.AllowAnonymousUsersToEmailAFriend)]
        public bool AllowAnonymousUsersToEmailAFriend { get; set; }
        public bool AllowAnonymousUsersToEmailAFriend_OverrideForStore { get; set; }

        [NopResourceDisplayName(Labels.SearchPageAllowCustomersToSelectPageSize)]
        public bool SearchPageAllowCustomersToSelectPageSize { get; set; }
        public bool SearchPageAllowCustomersToSelectPageSize_OverrideForStore { get; set; }

        [NopResourceDisplayName(Labels.SearchPagePageSizeOptions)]
        public string SearchPagePageSizeOptions { get; set; }
        public bool SearchPagePageSizeOptions_OverrideForStore { get; set; }

        [NopResourceDisplayName(Labels.ExportImport$Entity$sUsing$Entity$Name)]
        public bool ExportImport$Entity$sUsing$Entity$Name { get; set; }
        public bool ExportImport$Entity$sUsing$Entity$Name_OverrideForStore { get; set; }

        [NopResourceDisplayName(Labels.ExportImportAllowDownloadImages)]
        public bool ExportImportAllowDownloadImages { get; set; }
        public bool ExportImportAllowDownloadImages_OverrideForStore { get; set; }

        [NopResourceDisplayName(Labels.ExportImportRelatedEntitiesByName)]
        public bool ExportImportRelatedEntitiesByName { get; set; }
        public bool ExportImportRelatedEntitiesByName_OverrideForStore { get; set; }

        [NopResourceDisplayName(Labels.$Entity$BreadcrumbEnabled)]
        public bool $Entity$BreadcrumbEnabled { get; set; }
        public bool $Entity$BreadcrumbEnabled_OverrideForStore { get; set; }

        [NopResourceDisplayName(Labels.IgnoreAcl)]
        public bool IgnoreAcl { get; set; }

        [NopResourceDisplayName(Labels.IgnoreStoreLimitations)]
        public bool IgnoreStoreLimitations { get; set; }

        [NopResourceDisplayName(Labels.DisplayDatePreOrderAvailability)]
        public bool DisplayDatePreOrderAvailability { get; set; }
        public bool DisplayDatePreOrderAvailability_OverrideForStore { get; set; }

        public SortOptionSearchModel SortOptionSearchModel { get; set; }


        [NopResourceDisplayName(Labels.EnableSpecificationAttributeFiltering)]
        public bool EnableSpecificationAttributeFiltering { get; set; }
        public bool EnableSpecificationAttributeFiltering_OverrideForStore { get; set; }

        public string PrimaryStoreCurrencyCode { get; set; }
        [NopResourceDisplayName(Labels.DisplayAllPicturesOn$ucprojectname$Pages)]
        public bool DisplayAllPicturesOn$ucprojectname$Pages { get; set; }
        public bool DisplayAllPicturesOn$ucprojectname$Pages_OverrideForStore { get; set; }

        [NopResourceDisplayName(Labels.AllowCustomersToSearchWith$Entity$Name)]
        public bool AllowCustomersToSearchWith$Entity$Name { get; set; }
        public bool AllowCustomersToSearchWith$Entity$Name_OverrideForStore { get; set; }
        #endregion
    }
}