using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using $ucprojectname$.Areas.Admin.Controllers.Settings;
using Nop.Web.Framework.Mvc.Routing;

namespace $ucprojectname$.Web.Infrastructure
{
    public class RouteProvider : IRouteProvider
    {
        public int Priority => 0;

        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(
                name: $ucprojectname$Defaults.ConfigurationRouteName,
                pattern: "Admin/$Entity$Setting/Configure",
                new { Controller = "$Entity$Setting", action = $Entity$SettingController.ConfigureActionName }
            );
        }
    }
}
