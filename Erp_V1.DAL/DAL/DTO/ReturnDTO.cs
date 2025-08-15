using System.Collections.Generic;

namespace Erp_V1.DAL.DTO
{
    public class ReturnDTO
    {
        public List<ReturnDetailDTO> Returns { get; set; }

        // Optionally include related lists if needed elsewhere
        public List<ProductDetailDTO> Products { get; set; }
        public List<CustomerDetailDTO> Customers { get; set; }
        public List<CategoryDetailDTO> Categories { get; set; }
    }
}
