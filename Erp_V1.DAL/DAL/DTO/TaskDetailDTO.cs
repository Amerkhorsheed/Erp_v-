using System;

namespace Erp_V1.DAL.DTO
{
    public class TaskDetailDTO
    {
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? TaskStartDate { get; set; }
        public DateTime? TaskDeliveryDate { get; set; }
        public int taskStateID { get; set; }
        public string TaskStateName { get; set; }

        public int EmployeeID { get; set; }
        public int UserNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int PositionID { get; set; }
        public string PositionName { get; set; }
    }
}
