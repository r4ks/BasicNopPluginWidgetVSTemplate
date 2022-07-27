using System.Threading.Tasks;
using $ucprojectname$.Areas.Admin.Models.$ucprojectname$;
using $ucprojectname$.Core.Domains.$ucprojectname$;

namespace $ucprojectname$.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the $lcent$ model factory
    /// </summary>
    public partial interface I$Entity$ModelFactory
    {
        /// <summary>
        /// Prepare $lcent$ search model
        /// </summary>
        /// <param name="searchModel">$Entity$ search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$ search model
        /// </returns>
        Task<$Entity$SearchModel> Prepare$Entity$SearchModelAsync($Entity$SearchModel searchModel);

        /// <summary>
        /// Prepare paged $lcent$ list model
        /// </summary>
        /// <param name="searchModel">$Entity$ search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the $lcent$ list model
        /// </returns>
        Task<$Entity$ListModel> Prepare$Entity$ListModelAsync($Entity$SearchModel searchModel);

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
        Task<$Entity$Model> Prepare$Entity$ModelAsync($Entity$Model model, $Entity$ $lcent$, bool excludeProperties = false);

    }
}