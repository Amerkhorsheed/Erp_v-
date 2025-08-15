namespace Erp_V1.DAL.DTO
{
    public class SalaryDetailDTO
    {
        public int SalaryID { get; set; }
        public int EmployeeID { get; set; }
        public int UserNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public int PositionID { get; set; }
        public string PositionName { get; set; }

        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }

        public int Amount { get; set; }
        public int PreviousAmount { get; set; }
    }
}
