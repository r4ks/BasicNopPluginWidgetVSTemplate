using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Configuration;
using Nop.Core.Domain.Directory;
using Nop.Core.Infrastructure;
using Nop.Data;
using $ucprojectname$.Areas.Admin.Models.Settings;
using $ucprojectname$.Core.Domains.$ucprojectname$;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Gdpr;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Stores;
using Nop.Services.Themes;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Settings;
using Nop.Web.Framework.Factories;

namespace $ucprojectname$.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the setting model factory implementation
    /// </summary>
    public partial class $Entity$SettingModelFactory : I$Entity$SettingModelFactory
    {
        #region Fields

        private readonly AppSettings _appSettings;
        private readonly CurrencySettings _currencySettings;
        private readonly ICurrencyService _currencyService;
        private readonly INopDataProvider _dataProvider;
        private readonly INopFileProvider _fileProvider;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IGdprService _gdprService;
        private readonly ILocalizedModelFactory _localizedModelFactory;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IThemeProvider _themeProvider;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public $Entity$SettingModelFactory(AppSettings appSettings,
            CurrencySettings currencySettings,
            ICurrencyService currencyService,
            INopDataProvider dataProvider,
            INopFileProvider fileProvider,
            IDateTimeHelper dateTimeHelper,
            IGdprService gdprService,
            ILocalizedModelFactory localizedModelFactory,
            IGenericAttributeService genericAttributeService,
            ILanguageService languageService,
            ILocalizationService localizationService,
            IPictureService pictureService,
            ISettingService settingService,
            IStoreContext storeContext,
            IStoreService storeService,
            IThemeProvider themeProvider,
            IWorkContext workContext)
        {
            _appSettings = appSettings;
            _currencySettings = currencySettings;
            _currencyService = currencyService;
            _dataProvider = dataProvider;
            _fileProvider = fileProvider;
            _dateTimeHelper = dateTimeHelper;
            _gdprService = gdprService;
            _localizedModelFactory = localizedModelFactory;
            _genericAttributeService = genericAttributeService;
            _languageService = languageService;
            _localizationService = localizationService;
            _pictureService = pictureService;
            _settingService = settingService;
            _storeContext = storeContext;
            _storeService = storeService;
            _themeProvider = themeProvider;
            _workContext = workContext;
        }

        #endregion

        #region Methods
        public virtual async Task<$Entity$SettingsModel> Prepare$Entity$SettingsModelAsync($Entity$SettingsModel model = null)
        {
            //load settings for a chosen store scope
            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var hrSettings = await _settingService.LoadSettingAsync<$Entity$Settings>(storeId);

            //fill in model values from the entity
            model ??= hrSettings.ToSettingsModel<$Entity$SettingsModel>();

            //fill in additional values (not existing in the entity)
            model.ActiveStoreScopeConfiguration = storeId;
            model.PrimaryStoreCurrencyCode = (await _currencyService.GetCurrencyByIdAsync(_currencySettings.PrimaryStoreCurrencyId))?.CurrencyCode;
            model.AvailableViewModes.Add(new SelectListItem
            {
                Text = await _localizationService.GetResourceAsync($Entity$SettingsModel.Labels.Grid),
                Value = "grid"
            });
            model.AvailableViewModes.Add(new SelectListItem
            {
                Text = await _localizationService.GetResourceAsync($Entity$SettingsModel.Labels.List),
                Value = "list"
            });

            //fill in overridden values
            if (storeId > 0)
            {
                model.DefaultViewMode_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.DefaultViewMode, storeId);
                model.ShowShareButton_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.ShowShareButton, storeId);
                model.PageShareCode_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.PageShareCode, storeId);
                model.EmailAFriendEnabled_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.EmailAFriendEnabled, storeId);
                model.AllowAnonymousUsersToEmailAFriend_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.AllowAnonymousUsersToEmailAFriend, storeId);
                model.SearchPageAllowCustomersToSelectPageSize_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.SearchPageAllowCustomersToSelectPageSize, storeId);
                model.SearchPagePageSizeOptions_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.SearchPagePageSizeOptions, storeId);
                model.ExportImport$Entity$sUsing$Entity$Name_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.ExportImport$Entity$sUsing$Entity$Name, storeId);
                model.ExportImportAllowDownloadImages_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.ExportImportAllowDownloadImages, storeId);
                model.ExportImportRelatedEntitiesByName_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.ExportImportRelatedEntitiesByName, storeId);
                model.DisplayDatePreOrderAvailability_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.DisplayDatePreOrderAvailability, storeId);
                model.EnableSpecificationAttributeFiltering_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.EnableSpecificationAttributeFiltering, storeId);
                model.DisplayAllPicturesOn$ucprojectname$Pages_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.DisplayAllPicturesOn$ucprojectname$Pages, storeId);
                model.$Entity$BreadcrumbEnabled_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.$Entity$BreadcrumbEnabled, storeId);
                model.AllowCustomersToSearchWith$Entity$Name_OverrideForStore = await _settingService.SettingExistsAsync(hrSettings, x => x.AllowCustomersToSearchWith$Entity$Name, storeId);
            }

            //prepare nested search model
            await PrepareSortOptionSearchModelAsync(model.SortOptionSearchModel);

            return model;
        }

        /// <summary>
        /// Prepare sort option search model
        /// </summary>
        /// <param name="searchModel">Sort option search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the sort option search model
        /// </returns>
        protected virtual Task<SortOptionSearchModel> PrepareSortOptionSearchModelAsync(SortOptionSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize();

            return Task.FromResult(searchModel);
        }
        #endregion
    }
}