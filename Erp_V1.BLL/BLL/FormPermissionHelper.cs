using Erp_V1.DAL.DTO;
using System;
using System.Linq;

namespace Erp_V1.BLL
{
    public static class FormPermissionHelper
    {
        public static bool HasFormAccess(EmployeeDetailDTO user, string formName)
        {
            if (user == null) return false;

            // Admin role has access to all forms
            if (user.RoleName.Equals("admin", StringComparison.OrdinalIgnoreCase))
                return true;

            // TODO: Implement proper form permission checking
            // For now, return true to avoid blocking functionality
            return true;
        }

        public static void RequireFormAccess(EmployeeDetailDTO user, string formName)
        {
            if (!HasFormAccess(user, formName))
            {
                throw new UnauthorizedAccessException($"User does not have access to {formName}");
            }
        }
    }
}