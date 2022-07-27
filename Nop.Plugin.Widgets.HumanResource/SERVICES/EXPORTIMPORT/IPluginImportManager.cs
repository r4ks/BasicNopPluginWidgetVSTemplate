using System.IO;
using System.Threading.Tasks;

namespace $ucprojectname$.Services.ExportImport
{
    /// <summary>
    /// Import manager interface
    /// </summary>
    public partial interface IPluginImportManager
    {
        /// <summary>
        /// Import $lcent$s from XLSX file
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task Import$Entity$sFromXlsxAsync(Stream stream);

    }
}
