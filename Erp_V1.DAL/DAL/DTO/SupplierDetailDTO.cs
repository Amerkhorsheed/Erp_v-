using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Erp_V1.DAL.DTO
{
    public class SupplierDetailDTO
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string PhoneNumber { get; set; }
        public bool isDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        
    }
}


