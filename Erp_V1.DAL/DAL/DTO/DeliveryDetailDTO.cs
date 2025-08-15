using System;

namespace Erp_V1.DAL.DTO
{
    public class DeliveryDetailDTO
    {
        public int DeliveryID { get; set; }
        public int SalesID { get; set; }
        public int? DeliveryPersonID { get; set; }
        public string DeliveryPersonName { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }
}