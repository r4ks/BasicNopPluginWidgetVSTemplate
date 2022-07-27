using FluentValidation;
using Nop.Data.Mapping;
using $ucprojectname$.Areas.Admin.Models.$ucprojectname$;
using $ucprojectname$.Core.Domains.$ucprojectname$;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Web.Areas.Admin.Validators;
using Nop.Web.Framework.Validators;

namespace $ucprojectname$.Areas.Admin.Validators.$ucprojectname$
{
    public partial class $Entity$Validator : BaseNopValidator<$Entity$Model>
    {
        #region Labels
        public static class Labels
        {
            public const string NameRequired = "Admin.$ucprojectname$.$Entity$s.Fields.Name.Required";
            public const string PageSizeOptionsShouldHaveUniqueItems = "Admin.$ucprojectname$.$Entity$s.Fields.PageSizeOptions.ShouldHaveUniqueItems";
            public const string PageSizePositive = "Admin.$ucprojectname$.$Entity$s.Fields.PageSize.Positive";
            public const string SeNameMaxLengthValidation = "Admin.SEO.SeName.MaxLengthValidation";
        }
        #endregion
        public $Entity$Validator(ILocalizationService localizationService, IMappingEntityAccessor mappingEntityAccessor)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync(Labels.NameRequired));
            RuleFor(x => x.PageSizeOptions).Must(ValidatorUtilities.PageSizeOptionsValidator).WithMessageAwait(localizationService.GetResourceAsync(Labels.PageSizeOptionsShouldHaveUniqueItems));
            RuleFor(x => x.PageSize).Must((x, context) =>
            {
                if (!x.AllowCustomersToSelectPageSize && x.PageSize <= 0)
                    return false;

                return true;
            }).WithMessageAwait(localizationService.GetResourceAsync(Labels.PageSizePositive));
            RuleFor(x => x.SeName).Length(0, NopSeoDefaults.SearchEngineNameLength)
                .WithMessageAwait(localizationService.GetResourceAsync(Labels.SeNameMaxLengthValidation), NopSeoDefaults.SearchEngineNameLength);

            SetDatabaseValidationRules<$Entity$>(mappingEntityAccessor);
        }
    }
}