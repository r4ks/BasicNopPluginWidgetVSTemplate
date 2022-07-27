using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core;
using Nop.Core.Domain.Media;
using Nop.Core.Infrastructure;
using $ucprojectname$.Services.$ucprojectname$;
using $ucprojectname$.Services.ExportImport.Help;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Seo;
using Nop.Services.Stores;
using $ucprojectname$.Core.Domains.$ucprojectname$;

namespace $ucprojectname$.Services.ExportImport
{
    /// <summary>
    /// Import manager
    /// </summary>
    public partial class PluginImportManager : IPluginImportManager
    {
        #region Fields

        private readonly $Entity$Settings _hrSettings;
        private readonly I$Entity$Service _$lcent$Service;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;
        private readonly INopFileProvider _fileProvider;
        private readonly IPictureService _pictureService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IUrlRecordService _urlRecordService;

        #endregion

        #region Ctor

        public PluginImportManager($Entity$Settings $lcent$Settings,
            I$Entity$Service $lcent$Service,
            ICustomerActivityService customerActivityService,
            IHttpClientFactory httpClientFactory,
            ILocalizationService localizationService,
            ILogger logger,
            INopFileProvider fileProvider,
            IPictureService pictureService,
            IStoreMappingService storeMappingService,
            IUrlRecordService urlRecordService
            )
        {
            _hrSettings = $lcent$Settings;
            _$lcent$Service = $lcent$Service;
            _customerActivityService = customerActivityService;
            _httpClientFactory = httpClientFactory;
            _fileProvider = fileProvider;
            _localizationService = localizationService;
            _logger = logger;
            _pictureService = pictureService;
            _storeMappingService = storeMappingService;
            _urlRecordService = urlRecordService;
        }

        #endregion

        #region Utilities
        protected virtual string GetMimeTypeFromFilePath(string filePath)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(filePath, out var mimeType);

            //set to jpeg in case mime type cannot be found
            return mimeType ?? MimeTypes.ImageJpeg;
        }

        /// <summary>
        /// Creates or loads the image
        /// </summary>
        /// <param name="picturePath">The path to the image file</param>
        /// <param name="name">The name of the object</param>
        /// <param name="picId">Image identifier, may be null</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the image or null if the image has not changed
        /// </returns>
        protected virtual async Task<Picture> LoadPictureAsync(string picturePath, string name, int? picId = null)
        {
            if (string.IsNullOrEmpty(picturePath) || !_fileProvider.FileExists(picturePath))
                return null;

            var mimeType = GetMimeTypeFromFilePath(picturePath);
            var newPictureBinary = await _fileProvider.ReadAllBytesAsync(picturePath);
            var pictureAlreadyExists = false;
            if (picId != null)
            {
                //compare with existing product pictures
                var existingPicture = await _pictureService.GetPictureByIdAsync(picId.Value);
                if (existingPicture != null)
                {
                    var existingBinary = await _pictureService.LoadPictureBinaryAsync(existingPicture);
                    //picture binary after validation (like in database)
                    var validatedPictureBinary = await _pictureService.ValidatePictureAsync(newPictureBinary, mimeType, name);
                    if (existingBinary.SequenceEqual(validatedPictureBinary) ||
                        existingBinary.SequenceEqual(newPictureBinary))
                    {
                        pictureAlreadyExists = true;
                    }
                }
            }

            if (pictureAlreadyExists)
                return null;

            var newPicture = await _pictureService.InsertPictureAsync(newPictureBinary, mimeType, await _pictureService.GetPictureSeNameAsync(name));
            return newPicture;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task<(string seName, bool isParent$Entity$Exists)> Update$Entity$ByXlsxAsync($Entity$ $lcent$, PropertyManager<$Entity$> manager, Dictionary<string, ValueTask<$Entity$>> all$Entity$s, bool isNew)
        {
            var seName = string.Empty;
            var isParent$Entity$Exists = true;
            var isParent$Entity$Set = false;

            foreach (var property in manager.GetProperties)
            {
                switch (property.PropertyName)
                {
                    case "Name":
                        $lcent$.Name = property.StringValue.Split(new[] { ">>" }, StringSplitOptions.RemoveEmptyEntries).Last().Trim();
                        break;
                    case "Description":
                        $lcent$.Description = property.StringValue;
                        break;
                    case "Parent$Entity$Id":
                        if (!isParent$Entity$Set)
                        {
                            var parent$Entity$ = await await all$Entity$s.Values.FirstOrDefaultAwaitAsync(async c => (await c).Id == property.IntValue);
                            isParent$Entity$Set = parent$Entity$ != null;

                            isParent$Entity$Exists = isParent$Entity$Set || property.IntValue == 0;

                            $lcent$.Parent$Entity$Id = parent$Entity$?.Id ?? property.IntValue;
                        }

                        break;
                    case "Parent$Entity$Name":
                        if (_hrSettings.ExportImport$Entity$sUsing$Entity$Name && !isParent$Entity$Set)
                        {
                            var $lcent$Name = manager.GetProperty("Parent$Entity$Name").StringValue;
                            if (!string.IsNullOrEmpty($lcent$Name))
                            {
                                var parent$Entity$ = all$Entity$s.ContainsKey($lcent$Name)
                                    //try find $lcent$ by full name with all parent $lcent$ names
                                    ? await all$Entity$s[$lcent$Name]
                                    //try find $lcent$ by name
                                    : await await all$Entity$s.Values.FirstOrDefaultAwaitAsync(async c => (await c).Name.Equals($lcent$Name, StringComparison.InvariantCulture));

                                if (parent$Entity$ != null)
                                {
                                    $lcent$.Parent$Entity$Id = parent$Entity$.Id;
                                    isParent$Entity$Set = true;
                                }
                                else
                                {
                                    isParent$Entity$Exists = false;
                                }
                            }
                        }

                        break;
                    case "Picture":
                        var picture = await LoadPictureAsync(manager.GetProperty("Picture").StringValue, $lcent$.Name, isNew ? null : (int?)$lcent$.PictureId);
                        if (picture != null)
                            $lcent$.PictureId = picture.Id;
                        break;
                    case "PageSize":
                        $lcent$.PageSize = property.IntValue;
                        break;
                    case "AllowCustomersToSelectPageSize":
                        $lcent$.AllowCustomersToSelectPageSize = property.BooleanValue;
                        break;
                    case "PageSizeOptions":
                        $lcent$.PageSizeOptions = property.StringValue;
                        break;
                    case "ShowOnHomepage":
                        $lcent$.ShowOnHomepage = property.BooleanValue;
                        break;
                    case "IncludeInTopMenu":
                        $lcent$.IncludeInTopMenu = property.BooleanValue;
                        break;
                    case "Published":
                        $lcent$.Published = property.BooleanValue;
                        break;
                    case "DisplayOrder":
                        $lcent$.DisplayOrder = property.IntValue;
                        break;
                    case "SeName":
                        seName = property.StringValue;
                        break;
                }
            }

            $lcent$.UpdatedOnUtc = DateTime.UtcNow;
            return (seName, isParent$Entity$Exists);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task<($Entity$ $lcent$, bool isNew, string curent$Entity$BreadCrumb)> Get$Entity$FromXlsxAsync(PropertyManager<$Entity$> manager, IXLWorksheet worksheet, int iRow, Dictionary<string, ValueTask<$Entity$>> all$Entity$s)
        {
            manager.ReadFromXlsx(worksheet, iRow);

            //try get $lcent$ from database by ID
            var $lcent$ = await await all$Entity$s.Values.FirstOrDefaultAwaitAsync(async c => (await c).Id == manager.GetProperty("Id")?.IntValue);

            if (_hrSettings.ExportImport$Entity$sUsing$Entity$Name && $lcent$ == null)
            {
                var $lcent$Name = manager.GetProperty("Name").StringValue;
                if (!string.IsNullOrEmpty($lcent$Name))
                {
                    $lcent$ = all$Entity$s.ContainsKey($lcent$Name)
                        //try find $lcent$ by full name with all parent $lcent$ names
                        ? await all$Entity$s[$lcent$Name]
                        //try find $lcent$ by name
                        : await await all$Entity$s.Values.FirstOrDefaultAwaitAsync(async c => (await c).Name.Equals($lcent$Name, StringComparison.InvariantCulture));
                }
            }

            var isNew = $lcent$ == null;

            $lcent$ ??= new $Entity$();

            var curent$Entity$BreadCrumb = string.Empty;

            if (isNew)
            {
                $lcent$.CreatedOnUtc = DateTime.UtcNow;
                //default values
                $lcent$.PageSize = _hrSettings.Default$Entity$PageSize;
                $lcent$.PageSizeOptions = _hrSettings.Default$Entity$PageSizeOptions;
                $lcent$.Published = true;
                $lcent$.IncludeInTopMenu = true;
                $lcent$.AllowCustomersToSelectPageSize = true;
            }
            else
                curent$Entity$BreadCrumb = await _$lcent$Service.GetFormattedBreadCrumbAsync($lcent$);

            return ($lcent$, isNew, curent$Entity$BreadCrumb);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task Save$Entity$Async(bool isNew, $Entity$ $lcent$, Dictionary<string, ValueTask<$Entity$>> all$Entity$s, string curent$Entity$BreadCrumb, bool setSeName, string seName)
        {
            if (isNew)
                await _$lcent$Service.Insert$Entity$Async($lcent$);
            else
                await _$lcent$Service.Update$Entity$Async($lcent$);

            var $lcent$BreadCrumb = await _$lcent$Service.GetFormattedBreadCrumbAsync($lcent$);
            if (!all$Entity$s.ContainsKey($lcent$BreadCrumb))
                all$Entity$s.Add($lcent$BreadCrumb, new ValueTask<$Entity$>($lcent$));
            if (!string.IsNullOrEmpty(curent$Entity$BreadCrumb) && all$Entity$s.ContainsKey(curent$Entity$BreadCrumb) &&
                $lcent$BreadCrumb != curent$Entity$BreadCrumb)
                all$Entity$s.Remove(curent$Entity$BreadCrumb);

            //search engine name
            if (setSeName)
                await _urlRecordService.SaveSlugAsync($lcent$, await _urlRecordService.ValidateSeNameAsync($lcent$, seName, $lcent$.Name, true), 0);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get property list by excel cells
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="worksheet">Excel worksheet</param>
        /// <returns>Property list</returns>
        public static IList<PropertyByName<T>> GetPropertiesByExcelCells<T>(IXLWorksheet worksheet)
        {
            var properties = new List<PropertyByName<T>>();
            var poz = 1;
            while (true)
            {
                try
                {
                    var cell = worksheet.Row(1).Cell(poz);

                    if (string.IsNullOrEmpty(cell?.Value?.ToString()))
                        break;

                    poz += 1;
                    properties.Add(new PropertyByName<T>(cell.Value.ToString()));
                }
                catch
                {
                    break;
                }
            }

            return properties;
        }

        /// <summary>
        /// Import $lcent$s from XLSX file
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task Import$Entity$sFromXlsxAsync(Stream stream)
        {
            using var workboox = new XLWorkbook(stream);
            // get the first worksheet in the workbook
            var worksheet = workboox.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new NopException("No worksheet found");

            //the columns
            var properties = GetPropertiesByExcelCells<$Entity$>(worksheet);

            var manager = new PropertyManager<$Entity$>(properties, _hrSettings);

            var iRow = 2;
            var setSeName = properties.Any(p => p.PropertyName == "SeName");

            //performance optimization, load all $lcent$s in one SQL request
            var all$Entity$s = await (await _$lcent$Service
                .GetAll$Entity$sAsync(showHidden: true))
                .GroupByAwait(async c => await _$lcent$Service.GetFormattedBreadCrumbAsync(c))
                .ToDictionaryAsync(c => c.Key, c => c.FirstAsync());

            var saveNextTime = new List<int>();

            while (true)
            {
                var allColumnsAreEmpty = manager.GetProperties
                    .Select(property => worksheet.Row(iRow).Cell(property.PropertyOrderPosition))
                    .All(cell => string.IsNullOrEmpty(cell?.Value?.ToString()));

                if (allColumnsAreEmpty)
                    break;

                //get $lcent$ by data in xlsx file if it possible, or create new $lcent$
                var ($lcent$, isNew, current$Entity$BreadCrumb) = await Get$Entity$FromXlsxAsync(manager, worksheet, iRow, all$Entity$s);

                //update $lcent$ by data in xlsx file
                var (seName, isParent$Entity$Exists) = await Update$Entity$ByXlsxAsync($lcent$, manager, all$Entity$s, isNew);

                if (isParent$Entity$Exists)
                {
                    //if parent $lcent$ exists in database then save $lcent$ into database
                    await Save$Entity$Async(isNew, $lcent$, all$Entity$s, current$Entity$BreadCrumb, setSeName, seName);
                }
                else
                {
                    //if parent $lcent$ doesn't exists in database then try save $lcent$ into database next time
                    saveNextTime.Add(iRow);
                }

                iRow++;
            }

            var needSave = saveNextTime.Any();

            while (needSave)
            {
                var remove = new List<int>();

                //try to save unsaved $lcent$s
                foreach (var rowId in saveNextTime)
                {
                    //get $lcent$ by data in xlsx file if it possible, or create new $lcent$
                    var ($lcent$, isNew, current$Entity$BreadCrumb) = await Get$Entity$FromXlsxAsync(manager, worksheet, rowId, all$Entity$s);
                    //update $lcent$ by data in xlsx file
                    var (seName, isParent$Entity$Exists) = await Update$Entity$ByXlsxAsync($lcent$, manager, all$Entity$s, isNew);

                    if (!isParent$Entity$Exists)
                        continue;

                    //if parent $lcent$ exists in database then save $lcent$ into database
                    await Save$Entity$Async(isNew, $lcent$, all$Entity$s, current$Entity$BreadCrumb, setSeName, seName);
                    remove.Add(rowId);
                }

                saveNextTime.RemoveAll(item => remove.Contains(item));

                needSave = remove.Any() && saveNextTime.Any();
            }

            //activity log
            await _customerActivityService.InsertActivityAsync("Import$Entity$s",
                string.Format(await _localizationService.GetResourceAsync($ucprojectname$Defaults.Labels.Import$Entity$s), iRow - 2 - saveNextTime.Count));

            if (!saveNextTime.Any())
                return;

            var $lceplural$Name = new List<string>();

            foreach (var rowId in saveNextTime)
            {
                manager.ReadFromXlsx(worksheet, rowId);
                $lceplural$Name.Add(manager.GetProperty("Name").StringValue);
            }

            throw new ArgumentException(string.Format(await _localizationService.GetResourceAsync($ucprojectname$Defaults.Labels.$Entity$sArentImported), string.Join(", ", $lceplural$Name)));
        }

        #endregion

        #region Nested classes

        public class $Entity$Key
        {
            /// <returns>A task that represents the asynchronous operation</returns>
            public static async Task<$Entity$Key> Create$Entity$KeyAsync($Entity$ $lcent$, I$Entity$Service $lcent$Service, IList<$Entity$> all$Entity$s, IStoreMappingService storeMappingService)
            {
                return new $Entity$Key(await $lcent$Service.GetFormattedBreadCrumbAsync($lcent$, all$Entity$s), $lcent$.LimitedToStores ? (await storeMappingService.GetStoresIdsWithAccessAsync($lcent$)).ToList() : new List<int>())
                {
                    $Entity$ = $lcent$
                };
            }

            public $Entity$Key(string key, List<int> storesIds = null)
            {
                Key = key.Trim();
                StoresIds = storesIds ?? new List<int>();
            }

            public List<int> StoresIds { get; }

            public $Entity$ $Entity$ { get; private set; }

            public string Key { get; }

            public bool Equals($Entity$Key y)
            {
                if (y == null)
                    return false;

                if ($Entity$ != null && y.$Entity$ != null)
                    return $Entity$.Id == y.$Entity$.Id;

                if ((StoresIds.Any() || y.StoresIds.Any())
                    && (StoresIds.All(id => !y.StoresIds.Contains(id)) || y.StoresIds.All(id => !StoresIds.Contains(id))))
                    return false;

                return Key.Equals(y.Key);
            }

            public override int GetHashCode()
            {
                if (!StoresIds.Any())
                    return Key.GetHashCode();

                var storesIds = StoresIds.Select(id => id.ToString())
                    .Aggregate(string.Empty, (all, current) => all + current);

                return $"{storesIds}_{Key}".GetHashCode();
            }

            public override bool Equals(object obj)
            {
                var other = obj as $Entity$Key;
                return other?.Equals(other) ?? false;
            }
        }

        #endregion
    }
}