using System.Collections.Generic;
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DTO
{
    public class PermissionDTO
    {
        public List<PermissionDetailDTO> Permissions { get; set; }
        public List<PERMISSIONSTATE> States { get; set; }
    }
}
