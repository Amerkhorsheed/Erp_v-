using System;

namespace Erp_V1.DAL.DTO
{
    public class CustomerClassificationDTO
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; } 
        public string Tier { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
