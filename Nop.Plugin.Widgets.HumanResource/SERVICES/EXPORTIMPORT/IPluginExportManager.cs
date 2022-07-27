using System.Collections.Generic;
using System.Threading.Tasks;
using $ucprojectname$.Core.Domains.$ucprojectname$;

namespace $ucprojectname$.Services.ExportImport
{
    /// <summary>
    /// Export manager interface
    /// </summary>
    public partial interface IPluginExportManager
    {

        /// <summary>
        /// Export $lcent$ list to XML
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result in XML format
        /// </returns>
        Task<string> Export$Entity$sToXmlAsync();

        /// <summary>
        /// Export $lcent$s to XLSX
        /// </summary>
        /// <param name="$lcent$s">$Entity$s</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task<byte[]> Export$Entity$sToXlsxAsync(IList<$Entity$> $lcent$s);

    }
}
