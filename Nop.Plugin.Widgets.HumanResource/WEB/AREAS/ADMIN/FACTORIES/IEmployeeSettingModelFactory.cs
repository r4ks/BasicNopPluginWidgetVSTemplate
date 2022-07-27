using System.Threading.Tasks;
using Nop.Core.Domain.Gdpr;
using $ucprojectname$.Areas.Admin.Models.Settings;
using Nop.Web.Areas.Admin.Models.Settings;

namespace $ucprojectname$.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the setting model factory
    /// </summary>
    public partial interface I$Entity$SettingModelFactory
    {
        Task<$Entity$SettingsModel> Prepare$Entity$SettingsModelAsync($Entity$SettingsModel model = null);

        /// <summary>
        /// Prepare paged sort option list model
        /// </summary>
        /// <param name="searchModel">Sort option search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the sort option list model
        /// </returns>
    }
}