using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using System;

/// ViewComponent for Widgets
/// To be used to show charts or graphs on dashboard.
namespace $ucprojectname$.Web.Components
{
    [ViewComponent(Name = "StatsWidget")]
    public class $ucprojectname$StatsComponent : NopViewComponent
    {
        public $ucprojectname$StatsComponent()
        {

        }

        public IViewComponentResult Invoke(int productId)
        {
            return View("~/Plugins/Widgets.$ucprojectname$/Web/Views/$ucprojectname$StatsWidget.cshtml");
        }
    }
}
