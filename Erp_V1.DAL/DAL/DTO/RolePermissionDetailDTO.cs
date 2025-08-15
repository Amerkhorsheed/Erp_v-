using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp_V1.DAL.DTO
{
    public class RolePermissionDetailDTO
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string PermissionName { get; set; }
    }
}
