using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Data;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Stores;
using $ucprojectname$.Core.Domains.$ucprojectname$;

namespace $ucprojectname$.Services.$ucprojectname$
{
    /// <summary>
    /// $Entity$ service
    /// </summary>
    public partial class $Entity$Service : I$Entity$Service
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly IRepository<$Entity$> _$lcent$Repository;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IStoreContext _storeContext;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public $Entity$Service(
            IAclService aclService,
            ICustomerService customerService,
            ILocalizationService localizationService,
            IRepository<$Entity$> $lcent$Repository,
            IStaticCacheManager staticCacheManager,
            IStoreContext storeContext,
            IStoreMappingService storeMappingService,
            IWorkContext workContext)
        {
            _aclService = aclService;
            _customerService = customerService;
            _localizationService = localizationService;
            _$lcent$Repository = $lcent$Repository;
            _staticCacheManager = staticCacheManager;
            _storeContext = storeContext;
            _storeMappingService = storeMappingService;
            _workContext = workContext;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Sort $lcent$s for tree representation
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="parentId">Parent $lcent$ identifier</param>
        /// <param name="ignore$Entity$sWithoutExistingParent">A value indicating whether $lcent$s without parent $lcent$ in provided $lcent$ list (source) should be ignored</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the sorted $lcent$s
        /// </returns>
        protected virtual async Task<IList<$Entity$>> Sort$Entity$sForTreeAsync(IList<$Entity$> source, int parentId = 0,
            bool ignore$Entity$sWithoutExistingParent = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var result = new List<$Entity$>();

            foreach (var cat in source.Where(c => c.Parent$Entity$Id == parentId).ToList())
            {
                result.Add(cat);
                result.AddRange(await Sort$Entity$sForTreeAsync(source, cat.Id, true));
            }

            if (ignore$Entity$sWithoutExistingParent || result.Count == source.Count)
                return result;

            //find $lcent$s without parent in provided $lcent$ source and insert them into result
            foreach (var cat in source)
                if (result.FirstOrDefault(x => x.Id == cat.Id) == null)
                    result.Add(cat);

            return result;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete $lcent$
        /// </summary>
        /// <param name="$lcent$">$Entity$</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task Delete$Entity$Async($Entity$ $lcent$)
        {
            await _$lcent$Repository.DeleteAsync($lcent$);

            //reset a "Parent $lcent$" property of all child sub$lceplural$
            var sub$lceplural$ = await GetAll$Entity$sByParent$Entity$IdAsync($lcent$.Id, true);
            foreach (var sub$lcent$ in sub$lceplural$)
            {
                sub$lcent$.Parent$Entity$Id = 0;
                await Update$Entity$Async(sub$lcent$);
            }
        }

        /// <summary>
        /// Delete $Entity$s
        /// </summary>
        /// <param name="$lcent$s">$Entity$s</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task Delete$Entity$sAsync(IList<$Entity$> $lcent$s)
        {
            if ($lcent$s == null)
                throw new ArgumentNullException(nameof($lcent$s));

            foreach (var $lcent$ in $lcent$s)
                await Delete$Entity$Async($lcent$);
        }

        /// <summary>
        /// Gets all $lcent$s
        /// </summary>
        /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$s
        /// </returns>
        public virtual async Task<IList<$Entity$>> GetAll$Entity$sAsync(int storeId = 0, bool showHidden = false)
        {
            var key = _staticCacheManager.PrepareKeyForDefaultCache(Nop$Entity$Defaults.$Entity$sAllCacheKey,
                storeId,
                await _customerService.GetCustomerRoleIdsAsync(await _workContext.GetCurrentCustomerAsync()),
                showHidden);

            var $lcent$s = await _staticCacheManager
                .GetAsync(key, async () => (await GetAll$Entity$sAsync(string.Empty, storeId, showHidden: showHidden)).ToList());

            return $lcent$s;
        }

        /// <summary>
        /// Gets all $lcent$s
        /// </summary>
        /// <param name="$lcent$Name">$Entity$ name</param>
        /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <param name="overridePublished">
        /// null - process "Published" property according to "showHidden" parameter
        /// true - load only "Published" products
        /// false - load only "Unpublished" products
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$s
        /// </returns>
        public virtual async Task<IPagedList<$Entity$>> GetAll$Entity$sAsync(string $lcent$Name, int storeId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, bool? overridePublished = null)
        {
            var unsorted$Entity$s = await _$lcent$Repository.GetAllAsync(async query =>
            {
                if (!showHidden)
                    query = query.Where(c => c.Published);
                else if (overridePublished.HasValue)
                    query = query.Where(c => c.Published == overridePublished.Value);

                //apply store mapping constraints
                query = await _storeMappingService.ApplyStoreMapping(query, storeId);

                //apply ACL constraints
                if (!showHidden)
                {
                    var customer = await _workContext.GetCurrentCustomerAsync();
                    query = await _aclService.ApplyAcl(query, customer);
                }

                if (!string.IsNullOrWhiteSpace($lcent$Name))
                    query = query.Where(c => c.Name.Contains($lcent$Name));

                query = query.Where(c => !c.Deleted);

                return query.OrderBy(c => c.Parent$Entity$Id).ThenBy(c => c.DisplayOrder).ThenBy(c => c.Id);
            });

            //sort $lcent$s
            var sorted$Entity$s = await Sort$Entity$sForTreeAsync(unsorted$Entity$s);

            //paging
            return new PagedList<$Entity$>(sorted$Entity$s, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets all $lcent$s filtered by parent $lcent$ identifier
        /// </summary>
        /// <param name="parent$Entity$Id">Parent $lcent$ identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$s
        /// </returns>
        public virtual async Task<IList<$Entity$>> GetAll$Entity$sByParent$Entity$IdAsync(int parent$Entity$Id,
            bool showHidden = false)
        {
            var store = await _storeContext.GetCurrentStoreAsync();
            var customer = await _workContext.GetCurrentCustomerAsync();
            var $lcent$s = await _$lcent$Repository.GetAllAsync(async query =>
            {
                if (!showHidden)
                {
                    query = query.Where(c => c.Published);

                    //apply store mapping constraints
                    query = await _storeMappingService.ApplyStoreMapping(query, store.Id);

                    //apply ACL constraints
                    query = await _aclService.ApplyAcl(query, customer);
                }

                query = query.Where(c => !c.Deleted && c.Parent$Entity$Id == parent$Entity$Id);

                return query.OrderBy(c => c.DisplayOrder).ThenBy(c => c.Id);
            }, cache => cache.PrepareKeyForDefaultCache(Nop$Entity$Defaults.$Entity$sByParent$Entity$CacheKey,
                parent$Entity$Id, showHidden, customer, store));

            return $lcent$s;
        }

        /// <summary>
        /// Gets all $lcent$s displayed on the home page
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$s
        /// </returns>
        public virtual async Task<IList<$Entity$>> GetAll$Entity$sDisplayedOnHomepageAsync(bool showHidden = false)
        {
            var $lcent$s = await _$lcent$Repository.GetAllAsync(query =>
            {
                return from c in query
                       orderby c.DisplayOrder, c.Id
                       where c.Published &&
                             !c.Deleted &&
                             c.ShowOnHomepage
                       select c;
            }, cache => cache.PrepareKeyForDefaultCache(Nop$Entity$Defaults.$Entity$sHomepageCacheKey));

            if (showHidden)
                return $lcent$s;

            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(Nop$Entity$Defaults.$Entity$sHomepageWithoutHiddenCacheKey,
                await _storeContext.GetCurrentStoreAsync(), await _customerService.GetCustomerRoleIdsAsync(await _workContext.GetCurrentCustomerAsync()));

            var result = await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                return await $lcent$s
                    .WhereAwait(async c => await _aclService.AuthorizeAsync(c) && await _storeMappingService.AuthorizeAsync(c))
                    .ToListAsync();
            });

            return result;
        }

        /// <summary>
        /// Gets child $lcent$ identifiers
        /// </summary>
        /// <param name="parent$Entity$Id">Parent $lcent$ identifier</param>
        /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$ identifiers
        /// </returns>
        public virtual async Task<IList<int>> GetChild$Entity$IdsAsync(int parent$Entity$Id, int storeId = 0, bool showHidden = false)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(Nop$Entity$Defaults.$Entity$sChildIdsCacheKey,
                parent$Entity$Id,
                await _customerService.GetCustomerRoleIdsAsync(await _workContext.GetCurrentCustomerAsync()),
                storeId,
                showHidden);

            return await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                //little hack for performance optimization
                //there's no need to invoke "GetAll$Entity$sByParent$Entity$Id" multiple times (extra SQL commands) to load childs
                //so we load all $lcent$s at once (we know they are cached) and process them server-side
                var $lceplural$Ids = new List<int>();
                var $lcent$s = (await GetAll$Entity$sAsync(storeId: storeId, showHidden: showHidden))
                    .Where(c => c.Parent$Entity$Id == parent$Entity$Id)
                    .Select(c => c.Id)
                    .ToList();
                $lceplural$Ids.AddRange($lcent$s);
                $lceplural$Ids.AddRange(await $lcent$s.SelectManyAwait(async cId => await GetChild$Entity$IdsAsync(cId, storeId, showHidden)).ToListAsync());

                return $lceplural$Ids;
            });
        }

        /// <summary>
        /// Gets a $lcent$
        /// </summary>
        /// <param name="$lcent$Id">$Entity$ identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$
        /// </returns>
        public virtual async Task<$Entity$> Get$Entity$ByIdAsync(int $lcent$Id)
        {
            return await _$lcent$Repository.GetByIdAsync($lcent$Id, cache => default);
        }

        /// <summary>
        /// Inserts $lcent$
        /// </summary>
        /// <param name="$lcent$">$Entity$</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task Insert$Entity$Async($Entity$ $lcent$)
        {
            await _$lcent$Repository.InsertAsync($lcent$);
        }

        /// <summary>
        /// Updates the $lcent$
        /// </summary>
        /// <param name="$lcent$">$Entity$</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task Update$Entity$Async($Entity$ $lcent$)
        {
            if ($lcent$ == null)
                throw new ArgumentNullException(nameof($lcent$));

            //validate $lcent$ hierarchy
            var parent$Entity$ = await Get$Entity$ByIdAsync($lcent$.Parent$Entity$Id);
            while (parent$Entity$ != null)
            {
                if ($lcent$.Id == parent$Entity$.Id)
                {
                    $lcent$.Parent$Entity$Id = 0;
                    break;
                }

                parent$Entity$ = await Get$Entity$ByIdAsync(parent$Entity$.Parent$Entity$Id);
            }

            await _$lcent$Repository.UpdateAsync($lcent$);
        }



        /// <summary>
        /// Returns a list of names of not existing $lcent$s
        /// </summary>
        /// <param name="$lcent$IdsNames">The names and/or IDs of the $lcent$s to check</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of names and/or IDs not existing $lcent$s
        /// </returns>
        public virtual async Task<string[]> GetNotExisting$Entity$sAsync(string[] $lcent$IdsNames)
        {
            if ($lcent$IdsNames == null)
                throw new ArgumentNullException(nameof($lcent$IdsNames));

            var query = _$lcent$Repository.Table;
            var queryFilter = $lcent$IdsNames.Distinct().ToArray();
            //filtering by name
            var filter = await query.Select(c => c.Name)
                .Where(c => queryFilter.Contains(c))
                .ToListAsync();

             queryFilter = queryFilter.Except(filter).ToArray();

            //if some names not found
            if (!queryFilter.Any())
                return queryFilter.ToArray();

            //filtering by IDs
            filter = await query.Select(c => c.Id.ToString())
                .Where(c => queryFilter.Contains(c))
                .ToListAsync();

            return queryFilter.Except(filter).ToArray();
        }

        /// <summary>
        /// Gets $lcent$s by identifier
        /// </summary>
        /// <param name="$lcent$Ids">$Entity$ identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$s
        /// </returns>
        public virtual async Task<IList<$Entity$>> Get$Entity$sByIdsAsync(int[] $lcent$Ids)
        {
            return await _$lcent$Repository.GetByIdsAsync($lcent$Ids, includeDeleted: false);
        }

        /// <summary>
        /// Get formatted $lcent$ breadcrumb 
        /// Note: ACL and store mapping is ignored
        /// </summary>
        /// <param name="$lcent$">$Entity$</param>
        /// <param name="all$Entity$s">All $lcent$s</param>
        /// <param name="separator">Separator</param>
        /// <param name="languageId">Language identifier for localization</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the formatted breadcrumb
        /// </returns>
        public virtual async Task<string> GetFormattedBreadCrumbAsync($Entity$ $lcent$, IList<$Entity$> all$Entity$s = null,
            string separator = ">>", int languageId = 0)
        {
            var result = string.Empty;

            var breadcrumb = await Get$Entity$BreadCrumbAsync($lcent$, all$Entity$s, true);
            for (var i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var $lcent$Name = await _localizationService.GetLocalizedAsync(breadcrumb[i], x => x.Name, languageId);
                result = string.IsNullOrEmpty(result) ? $lcent$Name : $"{result} {separator} {$lcent$Name}";
            }

            return result;
        }

        /// <summary>
        /// Get $lcent$ breadcrumb 
        /// </summary>
        /// <param name="$lcent$">$Entity$</param>
        /// <param name="all$Entity$s">All $lcent$s</param>
        /// <param name="showHidden">A value indicating whether to load hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$ breadcrumb 
        /// </returns>
        public virtual async Task<IList<$Entity$>> Get$Entity$BreadCrumbAsync($Entity$ $lcent$, IList<$Entity$> all$Entity$s = null, bool showHidden = false)
        {
            if ($lcent$ == null)
                throw new ArgumentNullException(nameof($lcent$));

            var breadcrumbCacheKey = _staticCacheManager.PrepareKeyForDefaultCache(Nop$Entity$Defaults.$Entity$BreadcrumbCacheKey,
                $lcent$,
                await _customerService.GetCustomerRoleIdsAsync(await _workContext.GetCurrentCustomerAsync()),
                await _storeContext.GetCurrentStoreAsync(),
                await _workContext.GetWorkingLanguageAsync());

            return await _staticCacheManager.GetAsync(breadcrumbCacheKey, async () =>
            {
                var result = new List<$Entity$>();

                //used to prevent circular references
                var alreadyProcessed$Entity$Ids = new List<int>();

                while ($lcent$ != null && //not null
                       !$lcent$.Deleted && //not deleted
                       (showHidden || $lcent$.Published) && //published
                       (showHidden || await _aclService.AuthorizeAsync($lcent$)) && //ACL
                       (showHidden || await _storeMappingService.AuthorizeAsync($lcent$)) && //Store mapping
                       !alreadyProcessed$Entity$Ids.Contains($lcent$.Id)) //prevent circular references
                {
                    result.Add($lcent$);

                    alreadyProcessed$Entity$Ids.Add($lcent$.Id);

                    $lcent$ = all$Entity$s != null
                        ? all$Entity$s.FirstOrDefault(c => c.Id == $lcent$.Parent$Entity$Id)
                        : await Get$Entity$ByIdAsync($lcent$.Parent$Entity$Id);
                }

                result.Reverse();

                return result;
            });
        }

        /// <summary>
        /// Search $lcent$s
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="getOnlyTotalCount">A value in indicating whether you want to load only total number of records. Set to "true" if you don't want to load data from database</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$s
        /// </returns>
        public virtual async Task<IPagedList<$Entity$>> Search$Entity$sAsync(
            DateTime? createdFromUtc = null, DateTime? createdToUtc = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var query = _$lcent$Repository.Table;

            if (createdFromUtc.HasValue)
                query = query.Where(o => createdFromUtc.Value <= o.CreatedOnUtc);

            if (createdToUtc.HasValue)
                query = query.Where(o => createdToUtc.Value >= o.CreatedOnUtc);

            query = query.Where(o => !o.Deleted);
            query = query.OrderByDescending(o => o.CreatedOnUtc);

            //database layer paging
            return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
        }

        #endregion
    }
}