using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Caching;
using $ucprojectname$.Services.$ucprojectname$;
using $ucprojectname$.Web.Infrastructure.Cache;
using Nop.Services.Localization;
using Nop.Services.Stores;

namespace $ucprojectname$.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the implementation of the base model factory that implements a most common admin model factories methods
    /// </summary>
    public partial class PluginBaseAdminModelFactory : IPluginBaseAdminModelFactory
    {
        #region Fields

        private readonly I$Entity$Service _$lcent$Service;

        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IStoreService _storeService;

        #endregion

        #region Ctor

        public PluginBaseAdminModelFactory(I$Entity$Service $lcent$Service,
            ILocalizationService localizationService,
            IStaticCacheManager staticCacheManager,
            IStoreService storeService
            )
        {
            _$lcent$Service = $lcent$Service;
            _localizationService = localizationService;
            _staticCacheManager = staticCacheManager;
            _storeService = storeService;
        }

        public PluginBaseAdminModelFactory() { }
        #endregion

        #region Utilities

        /// <summary>
        /// Prepare default item
        /// </summary>
        /// <param name="items">Available items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use "All" text</param>
        /// <param name="defaultItemValue">Default item value; defaults 0</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task PrepareDefaultItemAsync(IList<SelectListItem> items, bool withSpecialDefaultItem, string defaultItemText = null, string defaultItemValue = "0")
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //whether to insert the first special item for the default value
            if (!withSpecialDefaultItem)
                return;

            //prepare item text
            defaultItemText ??= await _localizationService.GetResourceAsync($ucprojectname$Defaults.Labels.All);

            //insert this default item at first
            items.Insert(0, new SelectListItem { Text = defaultItemText, Value = defaultItemValue });
        }

        /// <summary>
        /// Get $lcent$ list
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$ list
        /// </returns>
        protected virtual async Task<List<SelectListItem>> Get$Entity$ListAsync(bool showHidden = true)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(NopModelCacheDefaults.$Entity$sListKey, showHidden);
            var listItems = await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                var $lcent$s = await _$lcent$Service.GetAll$Entity$sAsync(showHidden: showHidden);
                return await $lcent$s.SelectAwait(async c => new SelectListItem
                {
                    Text = await _$lcent$Service.GetFormattedBreadCrumbAsync(c, $lcent$s),
                    Value = c.Id.ToString()
                }).ToListAsync();
            });

            var result = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            foreach (var item in listItems)
                result.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });

            return result;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare available stores
        /// </summary>
        /// <param name="items">Store items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task PrepareStoresAsync(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available stores
            var availableStores = await _storeService.GetAllStoresAsync();
            foreach (var store in availableStores)
                items.Add(new SelectListItem { Value = store.Id.ToString(), Text = store.Name });

            //insert special item for the default value
            await PrepareDefaultItemAsync(items, withSpecialDefaultItem, defaultItemText);
        }


        /// <summary>
        /// Prepare available $lcent$s
        /// </summary>
        /// <param name="items">$Entity$ items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task Prepare$Entity$sAsync(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available $lcent$s
            var available$Entity$Items = await Get$Entity$ListAsync();
            foreach (var $lcent$Item in available$Entity$Items)
                items.Add($lcent$Item);

            //insert special item for the default value
            await PrepareDefaultItemAsync(items, withSpecialDefaultItem, defaultItemText);
        }

        #endregion
    }
}