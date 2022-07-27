using System.Collections.Generic;
using Nop.Core.Configuration;

namespace $ucprojectname$.Core.Domains.$ucprojectname$
{
    /// <summary>
    /// $ucprojectname$ settings
    /// </summary>
    public class $Entity$Settings : ISettings
    {
        public $Entity$Settings()
        {
        }

        /// <summary>
        /// Gets or sets a default view mode
        /// </summary>
        public string DefaultViewMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether $lcent$ breadcrumb is enabled
        /// </summary>
        public bool $Entity$BreadcrumbEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a 'Share button' is enabled
        /// </summary>
        public bool ShowShareButton { get; set; }

        /// <summary>
        /// Gets or sets a share code (e.g. AddThis button code)
        /// </summary>
        public string PageShareCode { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether product 'Email a friend' feature is enabled
        /// </summary>
        public bool EmailAFriendEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to allow anonymous users to email a friend.
        /// </summary>
        public bool AllowAnonymousUsersToEmailAFriend { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customers are allowed to select page size on the search products page
        /// </summary>
        public bool SearchPageAllowCustomersToSelectPageSize { get; set; }

        /// <summary>
        /// Gets or sets the available customer selectable page size options on the search products page
        /// </summary>
        public string SearchPagePageSizeOptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we should process attribute change using AJAX. It's used for dynamical attribute change, SKU/GTIN update of combinations, conditional attributes
        /// </summary>
        public bool AjaxProcessAttributeChange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore ACL rules (side-wide). It can significantly improve performance when enabled.
        /// </summary>
        public bool IgnoreAcl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore "limit per store" rules (side-wide). It can significantly improve performance when enabled.
        /// </summary>
        public bool IgnoreStoreLimitations { get; set; }

        /// <summary>
        /// Gets or sets the default value to use for $Entity$ page size options (for new $lcent$s)
        /// </summary>
        public string Default$Entity$PageSizeOptions { get; set; }

        /// <summary>
        /// Gets or sets the default value to use for $Entity$ page size (for new $lcent$s)
        /// </summary>
        public int Default$Entity$PageSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether need create dropdown list for export
        /// </summary>
        public bool ExportImportUseDropdownlistsForAssociatedEntities { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the $lcent$s need to be exported/imported using name of $lcent$
        /// </summary>
        public bool ExportImport$Entity$sUsing$Entity$Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the products should be exported/imported with a full $lcent$ name including names of all its parents
        /// </summary>
        public bool ExportImportProduct$Entity$Breadcrumb { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the images can be downloaded from remote server
        /// </summary>
        public bool ExportImportAllowDownloadImages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the related entities need to be exported/imported using name
        /// </summary>
        public bool ExportImportRelatedEntitiesByName { get; set; }

        /// <summary>
        /// Gets or sets count of displayed years for datepicker
        /// </summary>
        public int CountDisplayedYearsDatePicker { get; set; }

        /// <summary>
        /// Get or set a value indicating whether it's necessary to show the date for pre-order availability in a public store
        /// </summary>
        public bool DisplayDatePreOrderAvailability { get; set; }

        /// <summary>
        /// Get or set a value indicating whether use standart menu in public store or use Ajax to load menu
        /// </summary>
        public bool UseAjaxLoadMenu { get; set; }

        /// <summary>
        /// Get or set a value indicating whether the specification attribute filtering is enabled on hr pages
        /// </summary>
        public bool EnableSpecificationAttributeFiltering { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customer can search with $lcent$ name
        /// </summary>
        public bool AllowCustomersToSearchWith$Entity$Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether all pictures will be displayed on hr pages
        /// </summary>
        public bool DisplayAllPicturesOn$ucprojectname$Pages { get; set; }

    }
}