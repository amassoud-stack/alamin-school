using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.RolePermissions
{
    public class RolePermission : AuditableEntity
    {
        public Guid RoleId { get; set; }
        public string PermissionName { get; set; } = default!;
    }
}
