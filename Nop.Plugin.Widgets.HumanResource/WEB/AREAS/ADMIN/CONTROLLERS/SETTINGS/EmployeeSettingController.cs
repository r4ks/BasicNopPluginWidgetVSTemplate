using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using $ucprojectname$.Areas.Admin.Factories;
using $ucprojectname$.Areas.Admin.Models.Settings;
using $ucprojectname$.Core.Domains.$ucprojectname$;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

namespace $ucprojectname$.Areas.Admin.Controllers.Settings
{
    public partial class $Entity$SettingController : BaseAdminController
    {
        public const string ConfigureActionName = "Configure";
        #region Fields

        private readonly ICustomerActivityService _customerActivityService;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly I$Entity$SettingModelFactory _settingModelFactory;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        #endregion

        #region Ctor

        public $Entity$SettingController(
            ICustomerActivityService customerActivityService,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            I$Entity$SettingModelFactory settingModelFactory,
            ISettingService settingService,
            IStoreContext storeContext
            )
        {
            _customerActivityService = customerActivityService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingModelFactory = settingModelFactory;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        #endregion

        #region Methods
        [HttpGet, ActionName(ConfigureActionName)]
        public virtual async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            //prepare model
            var model = await _settingModelFactory.Prepare$Entity$SettingsModelAsync();

            return View($Entity$SettingsModel.View, model);
        }

        [HttpPost, ActionName(ConfigureActionName)]
        public virtual async Task<IActionResult> Configure($Entity$SettingsModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                //load settings for a chosen store scope
                var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
                var $lcent$Settings = await _settingService.LoadSettingAsync<$Entity$Settings>(storeScope);
                $lcent$Settings = model.ToSettings($lcent$Settings);

                //we do not clear cache after each setting update.
                //this behavior can increase performance because cached settings will not be cleared 
                //and loaded from database after each update
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.DefaultViewMode, model.DefaultViewMode_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.ShowShareButton, model.ShowShareButton_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.PageShareCode, model.PageShareCode_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.EmailAFriendEnabled, model.EmailAFriendEnabled_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.AllowAnonymousUsersToEmailAFriend, model.AllowAnonymousUsersToEmailAFriend_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.SearchPageAllowCustomersToSelectPageSize, model.SearchPageAllowCustomersToSelectPageSize_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.SearchPagePageSizeOptions, model.SearchPagePageSizeOptions_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.ExportImport$Entity$sUsing$Entity$Name, model.ExportImport$Entity$sUsing$Entity$Name_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.ExportImportAllowDownloadImages, model.ExportImportAllowDownloadImages_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.ExportImportRelatedEntitiesByName, model.ExportImportRelatedEntitiesByName_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.DisplayDatePreOrderAvailability, model.DisplayDatePreOrderAvailability_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.$Entity$BreadcrumbEnabled, model.$Entity$BreadcrumbEnabled_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.EnableSpecificationAttributeFiltering, model.EnableSpecificationAttributeFiltering_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.DisplayAllPicturesOn$ucprojectname$Pages, model.DisplayAllPicturesOn$ucprojectname$Pages_OverrideForStore, storeScope, false);
                await _settingService.SaveSettingOverridablePerStoreAsync($lcent$Settings, x => x.AllowCustomersToSearchWith$Entity$Name, model.AllowCustomersToSearchWith$Entity$Name_OverrideForStore, storeScope, false);

                //now settings not overridable per store
                await _settingService.SaveSettingAsync($lcent$Settings, x => x.IgnoreAcl, 0, false);

                //now clear settings cache
                await _settingService.ClearCacheAsync();

                //activity log
                await _customerActivityService.InsertActivityAsync("EditSettings", await _localizationService.GetResourceAsync($Entity$SettingsModel.Labels.EditSettings));

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync($Entity$SettingsModel.Labels.Updated));

                return RedirectToAction(ConfigureActionName);
            }

            //prepare model
            model = await _settingModelFactory.Prepare$Entity$SettingsModelAsync(model);

            //if we got this far, something failed, redisplay form
            return View($Entity$SettingsModel.View, model);
        }

        #endregion
    }
}