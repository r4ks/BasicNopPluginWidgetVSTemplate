using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Directory;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models.Extensions;
using $ucprojectname$.Services.$ucprojectname$;
using $ucprojectname$.Areas.Admin.Models.$ucprojectname$;
using $ucprojectname$.Core.Domains.$ucprojectname$;

namespace $ucprojectname$.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the $lcent$ model factory implementation
    /// </summary>
    public partial class $Entity$ModelFactory : I$Entity$ModelFactory
    {
        #region Fields

        private readonly $Entity$Settings _hrSettings;
        private readonly CurrencySettings _currencySettings;
        private readonly ICurrencyService _currencyService;
        private readonly IAclSupportedModelFactory _aclSupportedModelFactory;
        private readonly IPluginBaseAdminModelFactory _baseAdminModelFactory;
        private readonly I$Entity$Service _$lcent$Service;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedModelFactory _localizedModelFactory;
        private readonly IStoreMappingSupportedModelFactory _storeMappingSupportedModelFactory;
        private readonly IUrlRecordService _urlRecordService;

        #endregion

        #region Ctor

        public $Entity$ModelFactory($Entity$Settings $lcent$Settings,
            CurrencySettings currencySettings,
            ICurrencyService currencyService,
            IAclSupportedModelFactory aclSupportedModelFactory,
            IPluginBaseAdminModelFactory baseAdminModelFactory,
            I$Entity$Service $lcent$Service,
            ILocalizationService localizationService,
            ILocalizedModelFactory localizedModelFactory,
            IStoreMappingSupportedModelFactory storeMappingSupportedModelFactory,
            IUrlRecordService urlRecordService)
        {
            _hrSettings = $lcent$Settings;
            _currencySettings = currencySettings;
            _currencyService = currencyService;
            _aclSupportedModelFactory = aclSupportedModelFactory;
            _baseAdminModelFactory = baseAdminModelFactory;
            _$lcent$Service = $lcent$Service;
            _localizationService = localizationService;
            _localizedModelFactory = localizedModelFactory;
            _storeMappingSupportedModelFactory = storeMappingSupportedModelFactory;
            _urlRecordService = urlRecordService;
        }

        public $Entity$ModelFactory() { }
        #endregion

        #region Methods

        /// <summary>
        /// Prepare $lcent$ search model
        /// </summary>
        /// <param name="searchModel">$Entity$ search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$ search model
        /// </returns>
        public virtual async Task<$Entity$SearchModel> Prepare$Entity$SearchModelAsync($Entity$SearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare available stores
            await _baseAdminModelFactory.PrepareStoresAsync(searchModel.AvailableStores);

            searchModel.HideStoresList = _hrSettings.IgnoreStoreLimitations || searchModel.AvailableStores.SelectionIsNotPossible();

            //prepare "published" filter (0 - all; 1 - published only; 2 - unpublished only)
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "0",
                Text = await _localizationService.GetResourceAsync($Entity$SearchModel.Labels.All)
            });
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "1",
                Text = await _localizationService.GetResourceAsync($Entity$SearchModel.Labels.PublishedOnly)
            });
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "2",
                Text = await _localizationService.GetResourceAsync($Entity$SearchModel.Labels.UnpublishedOnly)
            });

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        /// <summary>
        /// Prepare paged $lcent$ list model
        /// </summary>
        /// <param name="searchModel">$Entity$ search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$ list model
        /// </returns>
        public virtual async Task<$Entity$ListModel> Prepare$Entity$ListModelAsync($Entity$SearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            //get $lcent$s
            var $lcent$s = await _$lcent$Service.GetAll$Entity$sAsync($lcent$Name: searchModel.Search$Entity$Name,
                showHidden: true,
                storeId: searchModel.SearchStoreId,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize,
                overridePublished: searchModel.SearchPublishedId == 0 ? null : searchModel.SearchPublishedId == 1);

            //prepare grid model
            var model = await new $Entity$ListModel().PrepareToGridAsync(searchModel, $lcent$s, () =>
            {
                return $lcent$s.SelectAwait(async $lcent$ =>
                {
                    //fill in model values from the entity
                    var $lcent$Model = $lcent$.ToModel<$Entity$Model>();

                    //fill in additional values (not existing in the entity)
                    $lcent$Model.Breadcrumb = await _$lcent$Service.GetFormattedBreadCrumbAsync($lcent$);
                    $lcent$Model.SeName = await _urlRecordService.GetSeNameAsync($lcent$, 0, true, false);

                    return $lcent$Model;
                });
            });

            return model;
        }

        /// <summary>
        /// Prepare $lcent$ model
        /// </summary>
        /// <param name="model">$Entity$ model</param>
        /// <param name="$lcent$">$Entity$</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$ model
        /// </returns>
        public virtual async Task<$Entity$Model> Prepare$Entity$ModelAsync($Entity$Model model, $Entity$ $lcent$, bool excludeProperties = false)
        {
            Func<$Entity$LocalizedModel, int, Task> localizedModelConfiguration = null;

            if ($lcent$ != null)
            {
                //fill in model values from the entity
                if (model == null)
                {
                    model = $lcent$.ToModel<$Entity$Model>();
                    model.SeName = await _urlRecordService.GetSeNameAsync($lcent$, 0, true, false);
                }

                //define localized model configuration action
                localizedModelConfiguration = async (locale, languageId) =>
                {
                    locale.Name = await _localizationService.GetLocalizedAsync($lcent$, entity => entity.Name, languageId, false, false);
                    locale.Description = await _localizationService.GetLocalizedAsync($lcent$, entity => entity.Description, languageId, false, false);
                    locale.SeName = await _urlRecordService.GetSeNameAsync($lcent$, languageId, false, false);
                };
            }

            //set default values for the new model
            if ($lcent$ == null)
            {
                model.PageSize = _hrSettings.Default$Entity$PageSize;
                model.PageSizeOptions = _hrSettings.Default$Entity$PageSizeOptions;
                model.Published = true;
                model.IncludeInTopMenu = true;
                model.AllowCustomersToSelectPageSize = true;
            }

            model.PrimaryStoreCurrencyCode = (await _currencyService.GetCurrencyByIdAsync(_currencySettings.PrimaryStoreCurrencyId)).CurrencyCode;

            //prepare localized models
            if (!excludeProperties)
                model.Locales = await _localizedModelFactory.PrepareLocalizedModelsAsync(localizedModelConfiguration);

            //prepare available parent $lcent$s
            await _baseAdminModelFactory.Prepare$Entity$sAsync(model.Available$Entity$s,
                defaultItemText: await _localizationService.GetResourceAsync($Entity$Model.Labels.None));

            //prepare model customer roles
            await _aclSupportedModelFactory.PrepareModelCustomerRolesAsync(model, $lcent$, excludeProperties);

            //prepare model stores
            await _storeMappingSupportedModelFactory.PrepareModelStoresAsync(model, $lcent$, excludeProperties);

            return model;
        }
        #endregion
    }
}