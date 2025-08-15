using System;

namespace Erp_V1.DAL.DTO
{
    public class PurchasesDetailDTO
    {
        public string SupplierName { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int SupplierID { get; set; }
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public int PurchaseAmount { get; set; }  // Mapped to PurchaseSalesAmout
        public int Price { get; set; }           // Mapped to PurchaseSalesPrice
        public DateTime PurchaseDate { get; set; }
        public int StockAmount { get; set; }
        public int PurchaseID { get; set; }
        public bool isCategoryDeleted { get; set; }
        public bool isProductDeleted { get; set; }
        public bool isSupplierDeleted { get; set; }
        public float MinQty { get; set; }
    }
}
