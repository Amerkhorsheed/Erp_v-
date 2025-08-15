using System.Collections.Generic;
using Erp_V1.DAL;   // bring in the EF MONTH entity
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DTO
{
    public class SalaryDTO
    {
        public List<SalaryDetailDTO> Salaries { get; set; }
        public List<EmployeeDetailDTO> Employees { get; set; }
        public List<DepartmentDetailDTO> Departments { get; set; }
        public List<PositionDetailDTO> Positions { get; set; }
        public List<MONTH> Months { get; set; }  // EF entity
    }
}
