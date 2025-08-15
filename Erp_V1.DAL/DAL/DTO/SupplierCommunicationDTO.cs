using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp_V1.DAL.DTO
{
    public class SupplierCommunicationDetailDTO
    {
        public int CommunicationID { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; } 
        public DateTime CommunicationDate { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string ReferenceLink { get; set; }
    }
    public class SupplierCommunicationDTO
    {
        public List<SupplierCommunicationDetailDTO> Communications { get; set; }
    }
}