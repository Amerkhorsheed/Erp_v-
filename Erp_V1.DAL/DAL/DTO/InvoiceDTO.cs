using Erp_V1.DAL.DTO;
using System.Collections.Generic;

namespace Erp_V1.DAL.DTO
{
    public class InvoiceDTO
    {
        public List<InvoiceDetailDTO> Invoices { get; set; }
        public List<CustomerDetailDTO> Customers { get; set; }
        public List<ProductDetailDTO> Products { get; set; }
        public List<CategoryDetailDTO> Categories { get; set; }
        public List<InvoiceItemDetailDTO> InvoiceItems { get; set; }
    }
}