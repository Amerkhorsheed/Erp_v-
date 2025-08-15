using System;

namespace Erp_V1.DAL.DTO
{
    public class ReturnDetailDTO
    {
        public int ReturnID { get; set; }
        public int SalesID { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public int ReturnQuantity { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ReturnReason { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
    }
}
