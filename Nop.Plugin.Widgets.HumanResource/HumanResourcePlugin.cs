using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core;
using $ucprojectname$.Areas.Admin.Models.$ucprojectname$;
using $ucprojectname$.Areas.Admin.Models.Settings;
using $ucprojectname$.Areas.Admin.Validators.$ucprojectname$;
using $ucprojectname$.Core.Domains.$ucprojectname$;
using $ucprojectname$.Services.Installation;
using $ucprojectname$.Services.Security;
using $ucprojectname$.Web.Components;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace $ucprojectname$
{
    /// <summary>
    /// Rename this file and change to the correct type
    /// </summary>
    public class $ucprojectname$Plugin : BasePlugin, IAdminMenuPlugin, IWidgetPlugin
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IExtraInstallationService _extraInstallationService;

        public bool HideInWidgetList => false;

        public $ucprojectname$Plugin(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor, IWebHelper webHelper, ISettingService settingService, ILocalizationService localizationService, IPermissionService permissionService, IExtraInstallationService extraInstallationService)
        {
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _webHelper = webHelper;
            _settingService = settingService;
            _localizationService = localizationService;
            _permissionService = permissionService;
            _extraInstallationService = extraInstallationService;
        }

        public override async Task InstallAsync()
        {
            //settings
            var regionInfo = new RegionInfo(NopCommonDefaults.DefaultLanguageCulture);
            var cultureInfo = new CultureInfo(NopCommonDefaults.DefaultLanguageCulture);
            await _extraInstallationService.InstallRequiredDataAsync(regionInfo, cultureInfo);

            await InstallLocalizedStringsAsync();
            await InstallPermissionsAsync();



            // Comment the following line to disable the installation of sample Data.
            //await _extraInstallationService.InstallSampleDataAsync();

            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            //settings
            await _settingService.DeleteSettingAsync<$Entity$Settings>();

            // locales
            await _localizationService.DeleteLocaleResourcesAsync("Admin.$ucprojectname$.$Entity$s");
            await base.UninstallAsync();
        }

        public override Task UpdateAsync(string currentVersion, string targetVersion)
        {
            return base.UpdateAsync(currentVersion, targetVersion);
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).RouteUrl($ucprojectname$Defaults.ConfigurationRouteName);
        }

        /// <summary>
        /// Add Menu Item on Admin area.
        /// </summary>
        /// <param name="rootNode"></param>
        /// <returns></returns>
        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            // Menu Item for Settings bellow Settings Category
            var $lcprojectname$SettingMenuItem = new SiteMapNode()
            {
                SystemName = $Entity$SettingsModel.SYSTEM_NAME,
                Title = "$Entity$s Setings",
                ActionName = "Configure",
                ControllerName = "$Entity$Setting",
                IconClass = "far fa-circle",
                Visible = true,
                RouteValues = new Microsoft.AspNetCore.Routing.RouteValueDictionary() { { "area", AreaNames.Admin } },
            };

            // Menu item for $Entity$ List below Human Resource Category
            var $lcent$ListMenuItem = new SiteMapNode()
            {
                SystemName = $Entity$SearchModel.SYSTEM_NAME,
                Title = "$Entity$ List",
                ControllerName = "$Entity$",
                ActionName = "List",
                IconClass = "fas fa-male",
                Visible = true,
                RouteValues = new Microsoft.AspNetCore.Routing.RouteValueDictionary() { { "area", AreaNames.Admin } },
            };

            // Add localized title and parent menu item
            var title = await _localizationService.GetResourceAsync($ucprojectname$Defaults.Labels.$ucprojectname$NodeTitle);
            // Human Resource Category Menu Item
            var mainNode = new SiteMapNode()
            {
                SystemName = "$ucprojectname$.MainNode",
                Title = title,
                IconClass = "fas fa-table",
                Visible = true,
            };

            rootNode.ChildNodes.Add(mainNode);

            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "$ucprojectname$.MainNode");
            var configurationNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Configuration");
            var settingsNode = configurationNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Settings");

            // Add human resource settings at Configuration > Settings
            // and before All Settings.
            if (settingsNode != null)
            {
                var last = settingsNode.ChildNodes.FirstOrDefault((i) => i.SystemName == "All settings");
                var index = settingsNode.ChildNodes.IndexOf(last);
                if(index != null)
                {
                    settingsNode.ChildNodes.Insert(index, $lcprojectname$SettingMenuItem);
                }
                else
                {
                    settingsNode.ChildNodes.Add($lcprojectname$SettingMenuItem);
                }
            }


            if (pluginNode != null)
            {
                pluginNode.ChildNodes.Add($lcent$ListMenuItem);
            }
            else
            {
                rootNode.ChildNodes.Add($lcent$ListMenuItem);
            }
        }

        /// <summary>
        /// Define here the zone of the widget you wished to be shown.
        /// </summary>
        /// <returns></returns>
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { AdminWidgetZones.DashboardTop });
        }

        /// <summary>
        /// If you want a widget you return the view from this method.
        /// </summary>
        /// <param name="widgetZone"></param>
        /// <returns></returns>
        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof($ucprojectname$StatsComponent);
        }

        #region Helpers
        /// <summary>
        /// Add Localized Strings into the Localized Dictionary from Localization Service.
        /// </summary>
        /// <returns></returns>
        private async Task InstallLocalizedStringsAsync()
        {
            //locales
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                [$Entity$Model.Labels.Name] = "Name",
                [$Entity$Model.Labels.Description] = "Description",
                [$Entity$Model.Labels.SeName] = "Search engine friendly page name",
                [$Entity$Model.Labels.Parent$Entity$Id] = "Parent $lcent$",
                [$Entity$Model.Labels.PictureId] = "Picture",
                [$Entity$Model.Labels.PageSize] = "Page size",
                [$Entity$Model.Labels.AllowCustomersToSelectPageSize] = "Allow customers to select page size",
                [$Entity$Model.Labels.PageSizeOptions] = "Page size options",
                [$Entity$Model.Labels.ShowOnHomepage] = "Show on home page",
                [$Entity$Model.Labels.IncludeInTopMenu] = "Include in top menu",
                [$Entity$Model.Labels.Published] = "Published",
                [$Entity$Model.Labels.Deleted] = "Deleted",
                [$Entity$Model.Labels.DisplayOrder] = "Display order",
                [$Entity$Model.Labels.SelectedCustomerRoleIds] = "Limited to customer roles",
                [$Entity$Model.Labels.SelectedStoreIds] = "Limited to stores",
                [$Entity$Model.Labels.None] = "[None]",
                [$Entity$Model.Labels.Edit$Entity$Details] = "Edit $lcent$ details",
                [$Entity$Model.Labels.BackToList] = "back to $lcent$ list",
                [$Entity$Model.Labels.AddNew] = "Add a new $lcent$",
                [$Entity$Model.Labels.Info] = "$Entity$ info",
                [$Entity$Model.Labels.Display] = "Display",
                [$Entity$Model.Labels.Mappings] = "Mappings",
                [$Entity$Model.Labels.Products] = "Products",
                [$Entity$Model.Labels.AddedEvent] = "The new $lcent$ has been added successfully.",
                [$Entity$Model.Labels.UpdatedEvent] = "The $lcent$ has been updated successfully.",
                [$Entity$Model.Labels.DeletedEvent] = "The $lcent$ has been deleted successfully.",
                [$Entity$Model.Labels.ImportedEvent] = "$Entity$s have been imported successfully.",
                [$Entity$Model.Labels.LogAddNew$Entity$] = "Added a new $lcent$ ('{0}')",
                [$Entity$Model.Labels.LogEdit$Entity$] = "Edited a $lcent$ ('{0}')",
                [$Entity$Model.Labels.LogDelete$Entity$] = "Deleted a $lcent$ ('{0}')",

                [$Entity$Model.Labels.Title] = "$Entity$s",

                [$Entity$SearchModel.Labels.Search$Entity$Name] = "$Entity$ name",
                [$Entity$SearchModel.Labels.SearchStoreId] = "Store",
                [$Entity$SearchModel.Labels.ImportFromExcelTip] = "Imported $lceplural$ are distinguished by ID. If the ID already exists, then its corresponding $lcent$ will be updated. You should not specify ID (leave 0) for new $lceplural$.",
                [$Entity$SearchModel.Labels.SearchPublishedId] = "Published",
                [$Entity$SearchModel.Labels.All] = "All",
                [$Entity$SearchModel.Labels.PublishedOnly] = "Published only",
                [$Entity$SearchModel.Labels.UnpublishedOnly] = "Unpublished only",
                [$Entity$SearchModel.Labels.DownloadPDF] = "Download List as PDF",

                [$Entity$SettingsModel.Labels.Title] = "$ucprojectname$ settings",
                [$Entity$SettingsModel.Labels.EditSettings] = "Edited settings",
                [$Entity$SettingsModel.Labels.Updated] = "The settings have been updated successfully.",
                [$Entity$SettingsModel.Labels.Grid] = "Grid",
                [$Entity$SettingsModel.Labels.List] = "List",
                [$Entity$SettingsModel.Labels.DefaultViewMode] = "Default view mode",
                [$Entity$SettingsModel.Labels.ShowShareButton] = "Show a share button",
                [$Entity$SettingsModel.Labels.PageShareCode] = "Share button code",
                [$Entity$SettingsModel.Labels.EmailAFriendEnabled] = "'Email a friend' enabled",
                [$Entity$SettingsModel.Labels.AllowAnonymousUsersToEmailAFriend] = "Allow anonymous users to email a friend",
                [$Entity$SettingsModel.Labels.SearchPageAllowCustomersToSelectPageSize] = "Search page. Allow customers to select page size",
                [$Entity$SettingsModel.Labels.SearchPagePageSizeOptions] = "Search page. Page size options",
                [$Entity$SettingsModel.Labels.ExportImport$Entity$sUsing$Entity$Name] = "Export/Import $lceplural$ using name of $lcent$",
                [$Entity$SettingsModel.Labels.ExportImportAllowDownloadImages] = "Export/Import products. Allow download images",
                [$Entity$SettingsModel.Labels.ExportImportRelatedEntitiesByName] = "Export/Import related entities using name",
                [$Entity$SettingsModel.Labels.$Entity$BreadcrumbEnabled] = "$Entity$ breadcrumb enabled",
                [$Entity$SettingsModel.Labels.IgnoreAcl] = "Ignore ACL rules (sitewide)",
                [$Entity$SettingsModel.Labels.IgnoreStoreLimitations] = "Ignore \"limit per store\" rules (sitewide)",
                [$Entity$SettingsModel.Labels.DisplayDatePreOrderAvailability] = "Display the date for a pre-order availability",
                [$Entity$SettingsModel.Labels.EnableSpecificationAttributeFiltering] = "Enable specification attribute filtering",
                [$Entity$SettingsModel.Labels.DisplayAllPicturesOn$ucprojectname$Pages] = "Display all pictures on hr pages",
                [$Entity$SettingsModel.Labels.AllowCustomersToSearchWith$Entity$Name] = "Allow customers to search with $lcent$ name",
                [$Entity$SettingsModel.Labels.Search] = "Search",
                [$Entity$SettingsModel.Labels.Performance] = "Performance",
                [$Entity$SettingsModel.Labels.Share] = "Share",
                [$Entity$SettingsModel.Labels.AdditionalSections] = "Additional sections",
                [$Entity$SettingsModel.Labels.$ucprojectname$Pages] = "$ucprojectname$ pages",
                [$Entity$SettingsModel.Labels.ExportImport] = "Export/Import",

                [$Entity$Validator.Labels.NameRequired] = "Please provide a name.",
                [$Entity$Validator.Labels.PageSizeOptionsShouldHaveUniqueItems] = "Page size options should not have duplicate items.",
                [$Entity$Validator.Labels.PageSizePositive] = "Page size should have a positive value.",
                [$Entity$Validator.Labels.SeNameMaxLengthValidation] = "Max length of search name is {0} chars",

                // Plugin's Dashboard Widget
                [$ucprojectname$Defaults.Labels.$Entity$Statistics] = "$Entity$s",
                [$ucprojectname$Defaults.Labels.OYear] = "Year",
                [$ucprojectname$Defaults.Labels.OMonth] = "Month",
                [$ucprojectname$Defaults.Labels.OWeek] = "Week",
                [$ucprojectname$Defaults.Labels.Import$Entity$s] = "{0} $lcent$s were imported",
                [$ucprojectname$Defaults.Labels.$Entity$sArentImported] = "$Entity$s with the following names aren't imported - {0}",

                [$ucprojectname$Defaults.Labels.$Entity$Statistics] = "$Entity$ Numbers",

                [$ucprojectname$Defaults.Labels.$ucprojectname$NodeTitle] = "Human Resource"

            });
        }

        /// <summary>
        /// Install Plugins Permissions
        /// </summary>
        /// <returns></returns>
        private async Task InstallPermissionsAsync()
        {
            //register default permissions
            var permissionProviders = new List<Type> { typeof(APermissionProvider) };
                    foreach (var providerType in permissionProviders)
                    {
                        var provider = (IPermissionProvider)Activator.CreateInstance(providerType);
            await _permissionService.InstallPermissionsAsync(provider);
        }
    }
    #endregion
}
}
