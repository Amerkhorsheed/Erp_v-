using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp_V1.DAL.DTO
{

    public class SupplyScheduleDetailDTO
    {
        public int ScheduleID { get; set; }
        public int ContractID { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public DateTime ExpectedDate { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class SupplyScheduleDTO
    {
        public List<SupplyScheduleDetailDTO> Schedules { get; set; }
    }


}
