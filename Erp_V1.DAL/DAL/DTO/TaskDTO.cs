using System.Collections.Generic;
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DTO
{
    public class TaskDTO
    {
        public List<EmployeeDetailDTO> Employees { get; set; }
        public List<DepartmentDetailDTO> Departments { get; set; }    
        public List<PositionDetailDTO> Positions { get; set; }    
        public List<TASKSTATE> TaskStates { get; set; }
        public List<TaskDetailDTO> Tasks { get; set; }
    }
}
