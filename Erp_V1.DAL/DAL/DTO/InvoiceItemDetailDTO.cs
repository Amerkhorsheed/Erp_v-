using System;

namespace Erp_V1.DAL.DTO
{
    public class InvoiceItemDetailDTO
    {
        public int ItemID { get; set; }
        public int InvoiceID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal LineTotal { get; set; }
        public bool isDeleted { get; set; }
        public bool isProductDeleted { get; set; }
        public bool isCategoryDeleted { get; set; }
        public int StockAmount { get; set; }
        public float MinQty { get; set; }
        public float MaxDiscount { get; set; }
    }
}