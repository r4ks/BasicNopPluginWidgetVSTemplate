using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Security;
using Nop.Services.Security;

namespace $ucprojectname$.Services.Security
{
        public class APermissionProvider : IPermissionProvider
        {
            private readonly PermissionRecord _permissionRecord = new()
            {
                Name = "Test name", SystemName = "Test permission record system name", Category = "Test $lcent$"
            };

            public IEnumerable<PermissionRecord> GetPermissions()
            {
                return new[] {
                    Manage$Entity$s,
                    _permissionRecord};
            }

            public static readonly PermissionRecord Manage$Entity$s = new() { Name = "Admin area. Manage $Entity$s", SystemName = "Manage$Entity$s", Category = "$ucprojectname$" };
            public HashSet<(string systemRoleName, PermissionRecord[] permissions)> GetDefaultPermissions()
            {
                return new()
                {
                    (
                        NopCustomerDefaults.AdministratorsRoleName,
                        new[] {
                            Manage$Entity$s,
                            _permissionRecord
                        }
                    )
                };
            }
        }
}
