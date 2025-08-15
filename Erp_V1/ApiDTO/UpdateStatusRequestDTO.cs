using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp_V1.ApiDTO
{
    public class UpdateStatusRequestDTO
    {
        public int DeliveryID { get; set; }
        public string Status { get; set; }
        public int DeliveryPersonID { get; set; } // To ensure the user can only update their own deliveries
    }
}
