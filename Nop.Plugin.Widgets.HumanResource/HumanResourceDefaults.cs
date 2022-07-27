using Nop.Core;

namespace $ucprojectname$
{
    public class $ucprojectname$Defaults
    {
        public static string SystemName => "Widgets.$ucprojectname$";

        public static string UserAgent => $"nopcommerce-{NopVersion.CURRENT_VERSION}";

        public static string ConfigurationRouteName => "Plugin.Widgets.$ucprojectname$.Areas.Admin.$Entity$Setting.Configure";

        // Common Labels
        public static class Labels {
            // Standard Localized String
            // *Recommended to not override these keys:
            public const string All = "Admin.Common.All";
            public const string AddNew = "Admin.Common.AddNew";
            public const string Export = "Admin.Common.Export";
            public const string ExportToXml = "Admin.Common.ExportToXml";
            public const string ExportToExcel = "Admin.Common.ExportToExcel";
            public const string Import = "Admin.Common.Import";
            public const string Selected = "Admin.Common.Delete.Selected";
            public const string Search = "Admin.Common.Search";
            public const string Edit = "Admin.Common.Edit";
            public const string NothingSelected = "Admin.Common.Alert.NothingSelected";
            public const string ImportFromExcel = "Admin.Common.ImportFromExcel";
            public const string ManyRecordsWarning = "Admin.Common.ImportFromExcel.ManyRecordsWarning";
            public const string ExcelFile = "Admin.Common.ExcelFile";
            public const string Save = "Admin.Common.Save";
            public const string SaveContinue = "Admin.Common.SaveContinue";
            public const string Preview = "Admin.Common.Preview";
            public const string Delete = "Admin.Common.Delete";
            public const string View = "Admin.Common.View";
            public const string SEO = "Admin.Common.SEO";
            public const string Saved = "Admin.Plugins.Saved";

            // Others
            public const string $Entity$Statistics = "Admin.$ucprojectname$.$Entity$s.$Entity$sStatistics";
            public const string OYear = "Admin.$ucprojectname$.$Entity$Statistics.Year";
            public const string OMonth = "Admin.$ucprojectname$.$Entity$Statistics.Month";
            public const string OWeek = "Admin.$ucprojectname$.$Entity$Statistics.Week";

            public const string Import$Entity$s = "ActivityLog.Import$Entity$s";
            public const string $Entity$sArentImported = "Admin.$ucprojectname$.$Entity$s.Import.$Entity$sArentImported";

            // Menu Item
            public const string $ucprojectname$NodeTitle = "$ucprojectname$.MainNode.Title";
        }
    }
}
