using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp_V1.DAL.DTO
{
    public class SupplierQuoteDetailDTO
    {
        public int QuoteID { get; set; }
        public Guid RequestID { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; } 
        public DateTime QuoteDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
        public string Details { get; set; }
    }
    public class SupplierQuoteDTO
    {
        public List<SupplierQuoteDetailDTO> Quotes { get; set; }
    }

}