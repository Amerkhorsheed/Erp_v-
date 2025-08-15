using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp_V1.DAL.DTO
{
    // --- Contracts
    public class SupplierContractDetailDTO
    {
        public int ContractID { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; } // This property is correctly defined
        public string ContractNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public string Terms { get; set; }
    }
    public class SupplierContractDTO
    {
        public List<SupplierContractDetailDTO> Contracts { get; set; }
    }
}
