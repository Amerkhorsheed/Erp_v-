using System;

namespace Erp_V1.DAL.DTO
{
    public class CustomerPointsDTO
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; } 
        public long Points { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
