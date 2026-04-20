using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Coonstants;

namespace Tahfeez.Application.Utilities
{
    public static class RoleUtilities
    {
        public static string GetRoleInString(UserRole role)
        {
            switch(role)
            {
                case UserRole.Admin:
                    return Roles.Admin;
                case UserRole.Teacher:
                    return Roles.Teacher;
                case UserRole.Student:
                    return Roles.Student;
                case UserRole.Parent:
                    return Roles.Parent;
                case UserRole.Accountant:
                    return Roles.Accountant;
                case UserRole.Assistant:
                    return Roles.Assistant;
                case UserRole.Supervisor:
                    return Roles.Supervisor;
                default:
                    return Roles.Student;
            }
        }
    }
}
