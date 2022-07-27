using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Nop.Core;
using $ucprojectname$.Services.$ucprojectname$;
using $ucprojectname$.Services.ExportImport.Help;
using Nop.Services.Common;
using Nop.Services.ExportImport;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Seo;
using $ucprojectname$.Core.Domains.$ucprojectname$;

namespace $ucprojectname$.Services.ExportImport
{
    /// <summary>
    /// Export manager
    /// </summary>
    public partial class PluginExportManager : IPluginExportManager
    {
        #region Fields

        private readonly $Entity$Settings _hrSettings;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly I$Entity$Service _$lcent$Service;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public PluginExportManager(
            $Entity$Settings $lcent$Settings,
            ICustomerActivityService customerActivityService,
            I$Entity$Service $lcent$Service,
            IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            IPictureService pictureService,
            IUrlRecordService urlRecordService,
            IWorkContext workContext
            )
        {
            _hrSettings = $lcent$Settings;
            _customerActivityService = customerActivityService;
            _$lcent$Service = $lcent$Service;
            _genericAttributeService = genericAttributeService;
            _localizationService = localizationService;
            _pictureService = pictureService;
            _urlRecordService = urlRecordService;
            _workContext = workContext;
        }

        #endregion

        #region Utilities

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task<int> Write$Entity$sAsync(XmlWriter xmlWriter, int parent$Entity$Id, int total$Entity$s)
        {
            var $lcent$s = await _$lcent$Service.GetAll$Entity$sByParent$Entity$IdAsync(parent$Entity$Id, true);
            if ($lcent$s == null || !$lcent$s.Any())
                return total$Entity$s;

            total$Entity$s += $lcent$s.Count;

            foreach (var $lcent$ in $lcent$s)
            {
                await xmlWriter.WriteStartElementAsync("$Entity$");

                await xmlWriter.WriteStringAsync("Id", $lcent$.Id);

                await xmlWriter.WriteStringAsync("Name", $lcent$.Name);
                await xmlWriter.WriteStringAsync("Description", $lcent$.Description);
                await xmlWriter.WriteStringAsync("SeName", await _urlRecordService.GetSeNameAsync($lcent$, 0), await IgnoreExport$Entity$PropertyAsync());
                await xmlWriter.WriteStringAsync("Parent$Entity$Id", $lcent$.Parent$Entity$Id);
                await xmlWriter.WriteStringAsync("PictureId", $lcent$.PictureId);
                await xmlWriter.WriteStringAsync("PageSize", $lcent$.PageSize, await IgnoreExport$Entity$PropertyAsync());
                await xmlWriter.WriteStringAsync("AllowCustomersToSelectPageSize", $lcent$.AllowCustomersToSelectPageSize, await IgnoreExport$Entity$PropertyAsync());
                await xmlWriter.WriteStringAsync("PageSizeOptions", $lcent$.PageSizeOptions, await IgnoreExport$Entity$PropertyAsync());
                await xmlWriter.WriteStringAsync("ShowOnHomepage", $lcent$.ShowOnHomepage, await IgnoreExport$Entity$PropertyAsync());
                await xmlWriter.WriteStringAsync("IncludeInTopMenu", $lcent$.IncludeInTopMenu, await IgnoreExport$Entity$PropertyAsync());
                await xmlWriter.WriteStringAsync("Published", $lcent$.Published, await IgnoreExport$Entity$PropertyAsync());
                await xmlWriter.WriteStringAsync("Deleted", $lcent$.Deleted, true);
                await xmlWriter.WriteStringAsync("DisplayOrder", $lcent$.DisplayOrder);
                await xmlWriter.WriteStringAsync("CreatedOnUtc", $lcent$.CreatedOnUtc, await IgnoreExport$Entity$PropertyAsync());
                await xmlWriter.WriteStringAsync("UpdatedOnUtc", $lcent$.UpdatedOnUtc, await IgnoreExport$Entity$PropertyAsync());

                await xmlWriter.WriteStartElementAsync("Products");

                await xmlWriter.WriteEndElementAsync();

                await xmlWriter.WriteStartElementAsync("Sub$Entity$s");
                total$Entity$s = await Write$Entity$sAsync(xmlWriter, $lcent$.Id, total$Entity$s);
                await xmlWriter.WriteEndElementAsync();
                await xmlWriter.WriteEndElementAsync();
            }

            return total$Entity$s;
        }

        /// <summary>
        /// Returns the path to the image file by ID
        /// </summary>
        /// <param name="pictureId">Picture ID</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the path to the image file
        /// </returns>
        protected virtual async Task<string> GetPicturesAsync(int pictureId)
        {
            var picture = await _pictureService.GetPictureByIdAsync(pictureId);

            return await _pictureService.GetThumbLocalPathAsync(picture);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task<bool> IgnoreExport$Entity$PropertyAsync()
        {
            try
            {
                return !await _genericAttributeService.GetAttributeAsync<bool>(await _workContext.GetCurrentCustomerAsync(), "$lcent$-advanced-mode");
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        /// <summary>
        /// Export $lcent$ list to XML
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result in XML format
        /// </returns>
        public virtual async Task<string> Export$Entity$sToXmlAsync()
        {
            var settings = new XmlWriterSettings
            {
                Async = true,
                ConformanceLevel = ConformanceLevel.Auto
            };

            await using var stringWriter = new StringWriter();
            await using var xmlWriter = XmlWriter.Create(stringWriter, settings);

            await xmlWriter.WriteStartDocumentAsync();
            await xmlWriter.WriteStartElementAsync("$Entity$s");
            await xmlWriter.WriteAttributeStringAsync("Version", NopVersion.CURRENT_VERSION);
            var total$Entity$s = await Write$Entity$sAsync(xmlWriter, 0, 0);
            await xmlWriter.WriteEndElementAsync();
            await xmlWriter.WriteEndDocumentAsync();
            await xmlWriter.FlushAsync();

            //activity log
            await _customerActivityService.InsertActivityAsync("Export$Entity$s",
                string.Format(await _localizationService.GetResourceAsync("ActivityLog.Export$Entity$s"), total$Entity$s));

            return stringWriter.ToString();
        }

        /// <summary>
        /// Export $lcent$s to XLSX
        /// </summary>
        /// <param name="$lcent$s">$Entity$s</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<byte[]> Export$Entity$sToXlsxAsync(IList<$Entity$> $lcent$s)
        {
            var parent$Entity$s = new List<$Entity$>();
            if (_hrSettings.ExportImport$Entity$sUsing$Entity$Name)
                //performance optimization, load all parent $lcent$s in one SQL request
                parent$Entity$s.AddRange(await _$lcent$Service.Get$Entity$sByIdsAsync($lcent$s.Select(c => c.Parent$Entity$Id).Where(id => id != 0).ToArray()));

            //property manager 
            var manager = new PropertyManager<$Entity$>(new[]
            {
                new PropertyByName<$Entity$>("Id", p => p.Id),
                new PropertyByName<$Entity$>("Name", p => p.Name),
                new PropertyByName<$Entity$>("Description", p => p.Description),
                new PropertyByName<$Entity$>("SeName", async p => await _urlRecordService.GetSeNameAsync(p, 0), await IgnoreExport$Entity$PropertyAsync()),
                new PropertyByName<$Entity$>("Parent$Entity$Id", p => p.Parent$Entity$Id),
                new PropertyByName<$Entity$>("Parent$Entity$Name", async p =>
                {
                    var $lcent$ = parent$Entity$s.FirstOrDefault(c => c.Id == p.Parent$Entity$Id);
                    return $lcent$ != null ? await _$lcent$Service.GetFormattedBreadCrumbAsync($lcent$) : null;

                }, !_hrSettings.ExportImport$Entity$sUsing$Entity$Name),
                new PropertyByName<$Entity$>("Picture", async p => await GetPicturesAsync(p.PictureId)),
                new PropertyByName<$Entity$>("PageSize", p => p.PageSize, await IgnoreExport$Entity$PropertyAsync()),
                new PropertyByName<$Entity$>("AllowCustomersToSelectPageSize", p => p.AllowCustomersToSelectPageSize, await IgnoreExport$Entity$PropertyAsync()),
                new PropertyByName<$Entity$>("PageSizeOptions", p => p.PageSizeOptions, await IgnoreExport$Entity$PropertyAsync()),
                new PropertyByName<$Entity$>("ShowOnHomepage", p => p.ShowOnHomepage, await IgnoreExport$Entity$PropertyAsync()),
                new PropertyByName<$Entity$>("IncludeInTopMenu", p => p.IncludeInTopMenu, await IgnoreExport$Entity$PropertyAsync()),
                new PropertyByName<$Entity$>("Published", p => p.Published, await IgnoreExport$Entity$PropertyAsync()),
                new PropertyByName<$Entity$>("DisplayOrder", p => p.DisplayOrder)
            }, _hrSettings);

            //activity log
            await _customerActivityService.InsertActivityAsync("Export$Entity$s",
                string.Format(await _localizationService.GetResourceAsync("ActivityLog.Export$Entity$s"), $lcent$s.Count));

            return await manager.ExportToXlsxAsync($lcent$s);
        }

         #endregion
    }
}
