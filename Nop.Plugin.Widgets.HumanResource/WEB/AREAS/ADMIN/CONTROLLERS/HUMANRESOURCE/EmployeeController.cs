using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using $ucprojectname$.Areas.Admin.Factories;
using $ucprojectname$.Areas.Admin.Models.$ucprojectname$;
using $ucprojectname$.Services.$ucprojectname$;
using $ucprojectname$.Services.ExportImport;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Mvc.Filters;
using $ucprojectname$.Services.Security;
using Nop.Services.Helpers;
using System.Globalization;
using Nop.Services.Orders;
using Nop.Web.Framework.Controllers;
using System.IO;
using $ucprojectname$.Core.Domains.$ucprojectname$;
using $ucprojectname$.Services.Common;

namespace $ucprojectname$.Web.Areas.Admin.Controllers.$ucprojectname$
{
    public partial class $Entity$Controller : BaseAdminController
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly I$Entity$ModelFactory _$lcent$ModelFactory;
        private readonly I$Entity$Service _$lcent$Service;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerService _customerService;
        private readonly IPluginExportManager _exportManager;
        private readonly IPluginImportManager _importManager;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IStoreService _storeService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWorkContext _workContext;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IPluginPdfService _pdfService;

        #endregion

        #region Ctor

        public $Entity$Controller(IAclService aclService,
            I$Entity$ModelFactory $lcent$ModelFactory,
            I$Entity$Service $lcent$Service,
            ICustomerActivityService customerActivityService,
            ICustomerService customerService,
            IPluginExportManager exportManager,
            IPluginImportManager importManager,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IPictureService pictureService,
            IStaticCacheManager staticCacheManager,
            IStoreMappingService storeMappingService,
            IStoreService storeService,
            IUrlRecordService urlRecordService,
            IWorkContext workContext,
            IDateTimeHelper dateTimeHelper,
            IPluginPdfService pdfService)
        {
            _aclService = aclService;
            _$lcent$ModelFactory = $lcent$ModelFactory;
            _$lcent$Service = $lcent$Service;
            _customerActivityService = customerActivityService;
            _customerService = customerService;
            _exportManager = exportManager;
            _importManager = importManager;
            _localizationService = localizationService;
            _localizedEntityService = localizedEntityService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _staticCacheManager = staticCacheManager;
            _storeMappingService = storeMappingService;
            _storeService = storeService;
            _urlRecordService = urlRecordService;
            _workContext = workContext;
            _dateTimeHelper = dateTimeHelper;
            _pdfService = pdfService;
        }

        #endregion

        #region Utilities

        protected virtual async Task UpdateLocalesAsync($Entity$ $lcent$, $Entity$Model model)
        {
            foreach (var localized in model.Locales)
            {
                await _localizedEntityService.SaveLocalizedValueAsync($lcent$,
                    x => x.Name,
                    localized.Name,
                    localized.LanguageId);

                await _localizedEntityService.SaveLocalizedValueAsync($lcent$,
                    x => x.Description,
                    localized.Description,
                    localized.LanguageId);

                //search engine name
                var seName = await _urlRecordService.ValidateSeNameAsync($lcent$, localized.SeName, localized.Name, false);
                await _urlRecordService.SaveSlugAsync($lcent$, seName, localized.LanguageId);
            }
        }

        protected virtual async Task UpdatePictureSeoNamesAsync($Entity$ $lcent$)
        {
            var picture = await _pictureService.GetPictureByIdAsync($lcent$.PictureId);
            if (picture != null)
                await _pictureService.SetSeoFilenameAsync(picture.Id, await _pictureService.GetPictureSeNameAsync($lcent$.Name));
        }

        protected virtual async Task Save$Entity$AclAsync($Entity$ $lcent$, $Entity$Model model)
        {
            $lcent$.SubjectToAcl = model.SelectedCustomerRoleIds.Any();
            await _$lcent$Service.Update$Entity$Async($lcent$);

            var existingAclRecords = await _aclService.GetAclRecordsAsync($lcent$);
            var allCustomerRoles = await _customerService.GetAllCustomerRolesAsync(true);
            foreach (var customerRole in allCustomerRoles)
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                    //new role
                    if (!existingAclRecords.Any(acl => acl.CustomerRoleId == customerRole.Id))
                        await _aclService.InsertAclRecordAsync($lcent$, customerRole.Id);
                    else
                    {
                        //remove role
                        var aclRecordToDelete = existingAclRecords.FirstOrDefault(acl => acl.CustomerRoleId == customerRole.Id);
                        if (aclRecordToDelete != null)
                            await _aclService.DeleteAclRecordAsync(aclRecordToDelete);
                    }
        }

        protected virtual async Task SaveStoreMappingsAsync($Entity$ $lcent$, $Entity$Model model)
        {
            $lcent$.LimitedToStores = model.SelectedStoreIds.Any();
            await _$lcent$Service.Update$Entity$Async($lcent$);

            var existingStoreMappings = await _storeMappingService.GetStoreMappingsAsync($lcent$);
            var allStores = await _storeService.GetAllStoresAsync();
            foreach (var store in allStores)
                if (model.SelectedStoreIds.Contains(store.Id))
                    //new store
                    if (!existingStoreMappings.Any(sm => sm.StoreId == store.Id))
                        await _storeMappingService.InsertStoreMappingAsync($lcent$, store.Id);
                    else
                    {
                        //remove store
                        var storeMappingToDelete = existingStoreMappings.FirstOrDefault(sm => sm.StoreId == store.Id);
                        if (storeMappingToDelete != null)
                            await _storeMappingService.DeleteStoreMappingAsync(storeMappingToDelete);
                    }
        }

        #endregion

        #region List

        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual async Task<IActionResult> List()
        {
            if (!await _permissionService.AuthorizeAsync(APermissionProvider.Manage$Entity$s))
                return AccessDeniedView();

            //prepare model
            var model = await _$lcent$ModelFactory.Prepare$Entity$SearchModelAsync(new $Entity$SearchModel());

            return View($Entity$SearchModel.LIST_VIEW, model);
            //return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> List($Entity$SearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(APermissionProvider.Manage$Entity$s))
                return await AccessDeniedDataTablesJson();

            //prepare model
            var model = await _$lcent$ModelFactory.Prepare$Entity$ListModelAsync(searchModel);

            return Json(model);
        }

        #endregion

        #region Create / Edit / Delete

        public virtual async Task<IActionResult> Create()
        {
            if (!await _permissionService.AuthorizeAsync(APermissionProvider.Manage$Entity$s))
                return AccessDeniedView();

            //prepare model
            var model = await _$lcent$ModelFactory.Prepare$Entity$ModelAsync(new $Entity$Model(), null);

            //return View(model);
            return View($Entity$Model.CREATE_VIEW, model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Create($Entity$Model model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(APermissionProvider.Manage$Entity$s))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var $lcent$ = model.ToEntity<$Entity$>();
                $lcent$.CreatedOnUtc = DateTime.UtcNow;
                $lcent$.UpdatedOnUtc = DateTime.UtcNow;
                await _$lcent$Service.Insert$Entity$Async($lcent$);

                //search engine name
                model.SeName = await _urlRecordService.ValidateSeNameAsync($lcent$, model.SeName, $lcent$.Name, true);
                await _urlRecordService.SaveSlugAsync($lcent$, model.SeName, 0);

                //locales
                await UpdateLocalesAsync($lcent$, model);

                await _$lcent$Service.Update$Entity$Async($lcent$);

                //update picture seo file name
                await UpdatePictureSeoNamesAsync($lcent$);

                //ACL (customer roles)
                await Save$Entity$AclAsync($lcent$, model);

                //stores
                await SaveStoreMappingsAsync($lcent$, model);

                //activity log
                await _customerActivityService.InsertActivityAsync("AddNew$Entity$",
                    string.Format(await _localizationService.GetResourceAsync($Entity$Model.Labels.LogAddNew$Entity$), $lcent$.Name), $lcent$);

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync($Entity$Model.Labels.AddedEvent));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = $lcent$.Id });
            }

            //prepare model
            model = await _$lcent$ModelFactory.Prepare$Entity$ModelAsync(model, null, true);

            //if we got this far, something failed, redisplay form
            return View(model);
        }

        public virtual async Task<IActionResult> Edit(int id)
        {
            if (!await _permissionService.AuthorizeAsync(APermissionProvider.Manage$Entity$s))
                return AccessDeniedView();

            //try to get a $lcent$ with the specified id
            var $lcent$ = await _$lcent$Service.Get$Entity$ByIdAsync(id);
            if ($lcent$ == null || $lcent$.Deleted)
                return RedirectToAction("List");

            //prepare model
            var model = await _$lcent$ModelFactory.Prepare$Entity$ModelAsync(null, $lcent$);

            //return View(model);
            return View($Entity$Model.EDIT_VIEW, model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Edit($Entity$Model model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(APermissionProvider.Manage$Entity$s))
                return AccessDeniedView();

            //try to get a $lcent$ with the specified id
            var $lcent$ = await _$lcent$Service.Get$Entity$ByIdAsync(model.Id);
            if ($lcent$ == null || $lcent$.Deleted)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                var prevPictureId = $lcent$.PictureId;

                //if parent $lcent$ changes, we need to clear cache for previous parent $lcent$
                if ($lcent$.Parent$Entity$Id != model.Parent$Entity$Id)
                {
                    await _staticCacheManager.RemoveByPrefixAsync(Nop$Entity$Defaults.$Entity$sByParent$Entity$Prefix, $lcent$.Parent$Entity$Id);
                    await _staticCacheManager.RemoveByPrefixAsync(Nop$Entity$Defaults.$Entity$sChildIdsPrefix, $lcent$.Parent$Entity$Id);
                }

                $lcent$ = model.ToEntity($lcent$);
                $lcent$.UpdatedOnUtc = DateTime.UtcNow;
                await _$lcent$Service.Update$Entity$Async($lcent$);

                //search engine name
                model.SeName = await _urlRecordService.ValidateSeNameAsync($lcent$, model.SeName, $lcent$.Name, true);
                await _urlRecordService.SaveSlugAsync($lcent$, model.SeName, 0);

                //locales
                await UpdateLocalesAsync($lcent$, model);

                await _$lcent$Service.Update$Entity$Async($lcent$);

                //delete an old picture (if deleted or updated)
                if (prevPictureId > 0 && prevPictureId != $lcent$.PictureId)
                {
                    var prevPicture = await _pictureService.GetPictureByIdAsync(prevPictureId);
                    if (prevPicture != null)
                        await _pictureService.DeletePictureAsync(prevPicture);
                }

                //update picture seo file name
                await UpdatePictureSeoNamesAsync($lcent$);

                //ACL
                await Save$Entity$AclAsync($lcent$, model);

                //stores
                await SaveStoreMappingsAsync($lcent$, model);

                //activity log
                await _customerActivityService.InsertActivityAsync("Edit$Entity$",
                    string.Format(await _localizationService.GetResourceAsync($Entity$Model.Labels.LogEdit$Entity$), $lcent$.Name), $lcent$);

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync($Entity$Model.Labels.UpdatedEvent));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = $lcent$.Id });
            }

            //prepare model
            model = await _$lcent$ModelFactory.Prepare$Entity$ModelAsync(model, $lcent$, true);

            //if we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            if (!await _permissionService.AuthorizeAsync(APermissionProvider.Manage$Entity$s))
                return AccessDeniedView();

            //try to get a $lcent$ with the specified id
            var $lcent$ = await _$lcent$Service.Get$Entity$ByIdAsync(id);
            if ($lcent$ == null)
                return RedirectToAction("List");

            await _$lcent$Service.Delete$Entity$Async($lcent$);

            //activity log
            await _customerActivityService.InsertActivityAsync("Delete$Entity$",
                string.Format(await _localizationService.GetResourceAsync($Entity$Model.Labels.LogDelete$Entity$), $lcent$.Name), $lcent$);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync($Entity$Model.Labels.DeletedEvent));

            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual async Task<IActionResult> DeleteSelected(ICollection<int> selectedIds)
        {
            if (!await _permissionService.AuthorizeAsync(APermissionProvider.Manage$Entity$s))
                return AccessDeniedView();

            if (selectedIds == null || selectedIds.Count == 0)
                return NoContent();

            await _$lcent$Service.Delete$Entity$sAsync(await (await _$lcent$Service.Get$Entity$sByIdsAsync(selectedIds.ToArray())).ToListAsync());

            return Json(new { Result = true });
        }

        #endregion

        #region Export / Import

        [HttpPost, ActionName("Download$ucprojectname$PDF")]
        [FormValueRequired("download-humanresource-pdf")]
        public virtual async Task<IActionResult> Download$ucprojectname$PDF($Entity$SearchModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            bool? overridePublished = null;
            if (model.SearchPublishedId == 1)
                overridePublished = true;
            else if (model.SearchPublishedId == 2)
                overridePublished = false;

            var products = await _$lcent$Service.Search$Entity$sAsync();

            try
            {
                byte[] bytes;
                await using (var stream = new MemoryStream())
                {
                    await _pdfService.Print$Entity$sToPdfAsync(stream, products);
                    bytes = stream.ToArray();
                }

                return File(bytes, MimeTypes.ApplicationPdf, "$lcent$s.pdf");
            }
            catch (Exception exc)
            {
                await _notificationService.ErrorNotificationAsync(exc);
                return RedirectToAction("List");
            }
        }

        public virtual async Task<IActionResult> ExportXml()
        {
            if (!await _permissionService.AuthorizeAsync(APermissionProvider.Manage$Entity$s))
                return AccessDeniedView();

            try
            {
                var xml = await _exportManager.Export$Entity$sToXmlAsync();

                return File(Encoding.UTF8.GetBytes(xml), "application/xml", "$lcent$s.xml");
            }
            catch (Exception exc)
            {
                await _notificationService.ErrorNotificationAsync(exc);
                return RedirectToAction("List");
            }
        }

        public virtual async Task<IActionResult> ExportXlsx()
        {
            if (!await _permissionService.AuthorizeAsync(APermissionProvider.Manage$Entity$s))
                return AccessDeniedView();

            try
            {
                var bytes = await _exportManager
                    .Export$Entity$sToXlsxAsync((await _$lcent$Service.GetAll$Entity$sAsync(showHidden: true)).ToList());

                return File(bytes, MimeTypes.TextXlsx, "$lcent$s.xlsx");
            }
            catch (Exception exc)
            {
                await _notificationService.ErrorNotificationAsync(exc);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> ImportFromXlsx(IFormFile importexcelfile)
        {
            if (!await _permissionService.AuthorizeAsync(APermissionProvider.Manage$Entity$s))
                return AccessDeniedView();

            try
            {
                if (importexcelfile != null && importexcelfile.Length > 0)
                    await _importManager.Import$Entity$sFromXlsxAsync(importexcelfile.OpenReadStream());
                else
                {
                    _notificationService.ErrorNotification(await _localizationService.GetResourceAsync($Entity$Model.Labels.LogUploadFile));
                    return RedirectToAction("List");
                }

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync($Entity$Model.Labels.ImportedEvent));

                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                await _notificationService.ErrorNotificationAsync(exc);
                return RedirectToAction("List");
            }
        }

        public virtual async Task<IActionResult> Load$Entity$Statistics(string period)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageOrders))
                return Content(string.Empty);

            var result = new List<object>();

            var nowDt = await _dateTimeHelper.ConvertToUserTimeAsync(DateTime.Now);
            var timeZone = await _dateTimeHelper.GetCurrentTimeZoneAsync();

            var culture = new CultureInfo((await _workContext.GetWorkingLanguageAsync()).LanguageCulture);

            switch (period)
            {
                case "year":
                    //year statistics
                    var yearAgoDt = nowDt.AddYears(-1).AddMonths(1);
                    var searchYearDateUser = new DateTime(yearAgoDt.Year, yearAgoDt.Month, 1);
                    for (var i = 0; i <= 12; i++)
                    {
                        result.Add(new
                        {
                            date = searchYearDateUser.Date.ToString("Y", culture),
                            value = (await _$lcent$Service.Search$Entity$sAsync(
                                createdFromUtc: _dateTimeHelper.ConvertToUtcTime(searchYearDateUser, timeZone),
                                createdToUtc: _dateTimeHelper.ConvertToUtcTime(searchYearDateUser.AddMonths(1), timeZone),
                                pageIndex: 0,
                                pageSize: 1, getOnlyTotalCount: true)).TotalCount.ToString()
                        });

                        searchYearDateUser = searchYearDateUser.AddMonths(1);
                    }

                    break;
                case "month":
                    //month statistics
                    var monthAgoDt = nowDt.AddDays(-30);
                    var searchMonthDateUser = new DateTime(monthAgoDt.Year, monthAgoDt.Month, monthAgoDt.Day);
                    for (var i = 0; i <= 30; i++)
                    {
                        result.Add(new
                        {
                            date = searchMonthDateUser.Date.ToString("M", culture),
                            value = (await _$lcent$Service.Search$Entity$sAsync(
                                createdFromUtc: _dateTimeHelper.ConvertToUtcTime(searchMonthDateUser, timeZone),
                                createdToUtc: _dateTimeHelper.ConvertToUtcTime(searchMonthDateUser.AddDays(1), timeZone),
                                pageIndex: 0,
                                pageSize: 1, getOnlyTotalCount: true)).TotalCount.ToString()
                        });

                        searchMonthDateUser = searchMonthDateUser.AddDays(1);
                    }

                    break;
                case "week":
                default:
                    //week statistics
                    var weekAgoDt = nowDt.AddDays(-7);
                    var searchWeekDateUser = new DateTime(weekAgoDt.Year, weekAgoDt.Month, weekAgoDt.Day);
                    for (var i = 0; i <= 7; i++)
                    {
                        result.Add(new
                        {
                            date = searchWeekDateUser.Date.ToString("d dddd", culture),
                            value = (await _$lcent$Service.Search$Entity$sAsync(
                                createdFromUtc: _dateTimeHelper.ConvertToUtcTime(searchWeekDateUser, timeZone),
                                createdToUtc: _dateTimeHelper.ConvertToUtcTime(searchWeekDateUser.AddDays(1), timeZone),
                                pageIndex: 0,
                                pageSize: 1, getOnlyTotalCount: true)).TotalCount.ToString()
                        });

                        searchWeekDateUser = searchWeekDateUser.AddDays(1);
                    }

                    break;
            }

            return Json(result);
        }
        #endregion

    }
}