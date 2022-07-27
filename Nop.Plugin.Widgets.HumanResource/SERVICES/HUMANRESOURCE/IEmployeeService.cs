using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using $ucprojectname$.Core.Domains.$ucprojectname$;

namespace $ucprojectname$.Services.$ucprojectname$
{
    /// <summary>
    /// $Entity$ service interface
    /// </summary>
    public partial interface I$Entity$Service
    {
        /// <summary>
        /// Delete $lcent$
        /// </summary>
        /// <param name="$lcent$">$Entity$</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task Delete$Entity$Async($Entity$ $lcent$);

        /// <summary>
        /// Gets all $lcent$s
        /// </summary>
        /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$s
        /// </returns>
        Task<IList<$Entity$>> GetAll$Entity$sAsync(int storeId = 0, bool showHidden = false);

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
        Task<IPagedList<$Entity$>> GetAll$Entity$sAsync(string $lcent$Name, int storeId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, bool? overridePublished = null);

        /// <summary>
        /// Gets all $lcent$s filtered by parent $lcent$ identifier
        /// </summary>
        /// <param name="parent$Entity$Id">Parent $lcent$ identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$s
        /// </returns>
        Task<IList<$Entity$>> GetAll$Entity$sByParent$Entity$IdAsync(int parent$Entity$Id, bool showHidden = false);

        /// <summary>
        /// Gets all $lcent$s displayed on the home page
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$s
        /// </returns>
        Task<IList<$Entity$>> GetAll$Entity$sDisplayedOnHomepageAsync(bool showHidden = false);

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
        Task<IList<int>> GetChild$Entity$IdsAsync(int parent$Entity$Id, int storeId = 0, bool showHidden = false);

        /// <summary>
        /// Gets a $lcent$
        /// </summary>
        /// <param name="$lcent$Id">$Entity$ identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$
        /// </returns>
        Task<$Entity$> Get$Entity$ByIdAsync(int $lcent$Id);

        /// <summary>
        /// Inserts $lcent$
        /// </summary>
        /// <param name="$lcent$">$Entity$</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task Insert$Entity$Async($Entity$ $lcent$);

        /// <summary>
        /// Updates the $lcent$
        /// </summary>
        /// <param name="$lcent$">$Entity$</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task Update$Entity$Async($Entity$ $lcent$);

        /// <summary>
        /// Delete a list of $lcent$s
        /// </summary>
        /// <param name="$lcent$s">$Entity$s</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task Delete$Entity$sAsync(IList<$Entity$> $lcent$s);

        /// <summary>
        /// Returns a list of names of not existing $lcent$s
        /// </summary>
        /// <param name="$lcent$IdsNames">The names and/or IDs of the $lcent$s to check</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of names and/or IDs not existing $lcent$s
        /// </returns>
        Task<string[]> GetNotExisting$Entity$sAsync(string[] $lcent$IdsNames);

        /// <summary>
        /// Gets $lcent$s by identifier
        /// </summary>
        /// <param name="$lcent$Ids">$Entity$ identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$s
        /// </returns>
        Task<IList<$Entity$>> Get$Entity$sByIdsAsync(int[] $lcent$Ids);


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
        Task<string> GetFormattedBreadCrumbAsync($Entity$ $lcent$, IList<$Entity$> all$Entity$s = null,
            string separator = ">>", int languageId = 0);

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
        Task<IList<$Entity$>> Get$Entity$BreadCrumbAsync($Entity$ $lcent$, IList<$Entity$> all$Entity$s = null, bool showHidden = false);

        /// <summary>
        /// Search $Entity$s
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
        Task<IPagedList<$Entity$>> Search$Entity$sAsync(
            DateTime? createdFromUtc = null, DateTime? createdToUtc = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);

    }
}