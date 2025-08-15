using Erp_V1.DAL.DTO;
using System.Collections.Generic;

public class PurchasesDTO
{
    public List<PurchasesDetailDTO> Purchases { get; set; }
    public List<ProductDetailDTO> Products { get; set; }
    public List<SupplierDetailDTO> Suppliers { get; set; }
    public List<CategoryDetailDTO> Categories { get; set; }
}
