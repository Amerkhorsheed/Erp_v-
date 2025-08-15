using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp_V1.DAL.DTO
{
    public class CustomerContractDTO
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ContractNumber { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }

        // <<< NEW PROPERTIES >>>
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
