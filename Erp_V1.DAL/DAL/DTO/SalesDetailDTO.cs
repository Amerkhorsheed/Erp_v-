using System;

namespace Erp_V1.DAL.DTO
{
    public class SalesDetailDTO
    {
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public int SalesAmount { get; set; }
        public int Price { get; set; }  // Mapped to ProductSalesPrice
        public DateTime SalesDate { get; set; }
        public int StockAmount { get; set; }
        public int SalesID { get; set; }
        public string TransactionId { get; set; }
        public bool isCategoryDeleted { get; set; }
        public bool isProductDeleted { get; set; }
        public bool isCustomerDeleted { get; set; }
        public float MinQty { get; set; }
        public float MaxDiscount { get; set; }

        // New fields for partial payment
        public long Madfou3 { get; set; } // Amount paid now
        public long Baky { get; set; }    // Remaining balance
    }
}
