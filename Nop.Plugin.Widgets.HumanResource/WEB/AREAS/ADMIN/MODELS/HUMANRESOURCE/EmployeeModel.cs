using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace $ucprojectname$.Areas.Admin.Models.$ucprojectname$
{
    /// <summary>
    /// Represents a $lcent$ model
    /// </summary>
    public partial record $Entity$Model : BaseNopEntityModel, IAclSupportedModel,
        ILocalizedModel<$Entity$LocalizedModel>, IStoreMappingSupportedModel
    {
        #region Views file path
        public const string CREATE_VIEW = "~/Plugins/Widgets.$ucprojectname$/Web/Areas/Admin/Views/$Entity$/Create.cshtml";
        public const string CREATE_OR_UPDATE_VIEW = "~/Plugins/Widgets.$ucprojectname$/Web/Areas/Admin/Views/$Entity$/_CreateOrUpdate.cshtml";
        public const string EDIT_VIEW = "~/Plugins/Widgets.$ucprojectname$/Web/Areas/Admin/Views/$Entity$/Edit.cshtml";
        #endregion

        #region Ctor

        public $Entity$Model()
        {
            if (PageSize < 1)
                PageSize = 5;

            Locales = new List<$Entity$LocalizedModel>();
            Available$Entity$s = new List<SelectListItem>();

            SelectedCustomerRoleIds = new List<int>();
            AvailableCustomerRoles = new List<SelectListItem>();

            SelectedStoreIds = new List<int>();
            AvailableStores = new List<SelectListItem>();

        }

        #endregion
        #region Labels
        public static partial class Labels
        {
            public const string Name = "Admin.$ucprojectname$.$Entity$s.Fields.Name";
            public const string Description = "Admin.$ucprojectname$.$Entity$s.Fields.Description";
            public const string SeName = "Admin.$ucprojectname$.$Entity$s.Fields.SeName";
            public const string Parent$Entity$Id = "Admin.$ucprojectname$.$Entity$s.Fields.Parent";
            public const string PictureId = "Admin.$ucprojectname$.$Entity$s.Fields.Picture";
            public const string PageSize = "Admin.$ucprojectname$.$Entity$s.Fields.PageSize";
            public const string AllowCustomersToSelectPageSize = "Admin.$ucprojectname$.$Entity$s.Fields.AllowCustomersToSelectPageSize";
            public const string PageSizeOptions = "Admin.$ucprojectname$.$Entity$s.Fields.PageSizeOptions";
            public const string ShowOnHomepage = "Admin.$ucprojectname$.$Entity$s.Fields.ShowOnHomepage";
            public const string IncludeInTopMenu = "Admin.$ucprojectname$.$Entity$s.Fields.IncludeInTopMenu";
            public const string Published = "Admin.$ucprojectname$.$Entity$s.Fields.Published";
            public const string Deleted = "Admin.$ucprojectname$.$Entity$s.Fields.Deleted";
            public const string DisplayOrder = "Admin.$ucprojectname$.$Entity$s.Fields.DisplayOrder";
            public const string SelectedCustomerRoleIds = "Admin.$ucprojectname$.$Entity$s.Fields.AclCustomerRoles";
            public const string SelectedStoreIds = "Admin.$ucprojectname$.$Entity$s.Fields.LimitedToStores";
            public const string None = "Admin.$ucprojectname$.$Entity$s.Fields.Parent.None";

            //View Labels:
            public const string Title = "Admin.$ucprojectname$.$Entity$s";
            public const string Edit$Entity$Details = "Admin.$ucprojectname$.$Entity$s.Edit$Entity$Details";
            public const string BackToList = "Admin.$ucprojectname$.$Entity$s.BackToList";
            public const string AddNew = "Admin.$ucprojectname$.$Entity$s.AddNew";
            public const string Info = "Admin.$ucprojectname$.$Entity$s.Info";
            public const string Display = "Admin.$ucprojectname$.$Entity$s.Display";
            public const string Mappings = "Admin.$ucprojectname$.$Entity$s.Mappings";
            public const string Products = "Admin.$ucprojectname$.$Entity$s.Products";

            // Events
            public const string AddedEvent = "Admin.$ucprojectname$.$Entity$s.Added";
            public const string UpdatedEvent = "Admin.$ucprojectname$.$Entity$s.Updated";
            public const string DeletedEvent = "Admin.$ucprojectname$.$Entity$s.Deleted";
            public const string ImportedEvent = "Admin.$ucprojectname$.$Entity$s.Imported";

            // Notifications
            public const string LogAddNew$Entity$ = "ActivityLog.AddNew$Entity$";
            public const string LogEdit$Entity$ = "ActivityLog.Edit$Entity$";
            public const string LogDelete$Entity$ = "ActivityLog.Delete$Entity$";

            // Don't override this please
            public const string LogUploadFile = "Admin.Common.UploadFile";
        }
        #endregion

        #region Properties

        [NopResourceDisplayName(Labels.Name)]
        public string Name { get; set; }

        [NopResourceDisplayName(Labels.Description)]
        public string Description { get; set; }

        [NopResourceDisplayName(Labels.SeName)]
        public string SeName { get; set; }

        [NopResourceDisplayName(Labels.Parent$Entity$Id)]
        public int Parent$Entity$Id { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName(Labels.PictureId)]
        public int PictureId { get; set; }

        [NopResourceDisplayName(Labels.PageSize)]
        public int PageSize { get; set; }

        [NopResourceDisplayName(Labels.AllowCustomersToSelectPageSize)]
        public bool AllowCustomersToSelectPageSize { get; set; }

        [NopResourceDisplayName(Labels.PageSizeOptions)]
        public string PageSizeOptions { get; set; }

        [NopResourceDisplayName(Labels.ShowOnHomepage)]
        public bool ShowOnHomepage { get; set; }

        [NopResourceDisplayName(Labels.IncludeInTopMenu)]
        public bool IncludeInTopMenu { get; set; }

        [NopResourceDisplayName(Labels.Published)]
        public bool Published { get; set; }

        [NopResourceDisplayName(Labels.Deleted)]
        public bool Deleted { get; set; }

        [NopResourceDisplayName(Labels.DisplayOrder)]
        public int DisplayOrder { get; set; }

        public IList<$Entity$LocalizedModel> Locales { get; set; }

        public string Breadcrumb { get; set; }

        //ACL (customer roles)
        [NopResourceDisplayName(Labels.SelectedCustomerRoleIds)]
        public IList<int> SelectedCustomerRoleIds { get; set; }
        public IList<SelectListItem> AvailableCustomerRoles { get; set; }

        //store mapping
        [NopResourceDisplayName(Labels.SelectedStoreIds)]
        public IList<int> SelectedStoreIds { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }

        public IList<SelectListItem> Available$Entity$s { get; set; }

        public string PrimaryStoreCurrencyCode { get; set; }

        #endregion
    }

    public partial record $Entity$LocalizedModel : ILocalizedLocaleModel
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName($Entity$Model.Labels.Name)]
        public string Name { get; set; }

        [NopResourceDisplayName($Entity$Model.Labels.Description)]
        public string Description { get; set; }

        [NopResourceDisplayName($Entity$Model.Labels.SeName)]
        public string SeName { get; set; }
    }
}