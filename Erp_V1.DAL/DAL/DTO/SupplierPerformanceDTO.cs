using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp_V1.DAL.DTO
{
    public class SupplierPerformanceDetailDTO
    {
        public int PerformanceID { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; } 
        public DateTime EvaluationDate { get; set; }
        public decimal Score { get; set; }
        public string ParameterDetails { get; set; }
        public string Comments { get; set; }
    }
    public class SupplierPerformanceDTO
    {
        public List<SupplierPerformanceDetailDTO> Performances { get; set; }
    }
}