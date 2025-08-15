using System;

namespace Erp_V1.DAL.DTO
{
    public class CustomerInteractionDTO
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; } 
        public string Type { get; set; }
        public string Notes { get; set; }
        public DateTime InteractionDate { get; set; }
    }
}
