// File: SalaryBLL.cs
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using System.Linq;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    public class SalaryBLL : IBLL<SalaryDetailDTO, SalaryDTO>
    {
        private readonly SalaryDAO _salDao = new SalaryDAO();
        private readonly EmployeeDAO _empDao = new EmployeeDAO();
        private readonly DepartmentDAO _deptDao = new DepartmentDAO();
        private readonly PositionDAO _posDao = new PositionDAO();

        public bool Insert(SalaryDetailDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.EmployeeID <= 0) throw new ArgumentException("A valid employee must be selected.", nameof(dto.EmployeeID));
            if (dto.Amount <= 0) throw new ArgumentException("Salary amount must be greater than zero.", nameof(dto.Amount));

            // ** FEATURE: Prevent duplicate salary entries for the same employee, month, and year **
            if (_salDao.Exists(dto.EmployeeID, dto.MonthID, dto.Year))
            {
                throw new InvalidOperationException("A salary for this employee for the selected month and year already exists.");
            }

            var entity = new SALARY
            {
                EmployeeID = dto.EmployeeID,
                MonthID = dto.MonthID,
                Year = dto.Year,
                Amount = dto.Amount
            };

            bool isInserted = _salDao.Insert(entity);
            if (isInserted)
            {
                // Cascade update of the employee's current salary in their main profile
                _empDao.UpdateSalary(dto.EmployeeID, dto.Amount);
            }
            return isInserted;
        }

        public bool Update(SalaryDetailDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.SalaryID <= 0) throw new ArgumentException("A valid SalaryID is required for update.", nameof(dto.SalaryID));
            if (dto.Amount <= 0) throw new ArgumentException("Salary amount must be greater than zero.", nameof(dto.Amount));

            var entity = new SALARY
            {
                ID = dto.SalaryID,
                MonthID = dto.MonthID,
                Year = dto.Year,
                Amount = dto.Amount
            };

            bool isUpdated = _salDao.Update(entity);
            if (isUpdated)
            {
                // Also update the employee's main salary field
                _empDao.UpdateSalary(dto.EmployeeID, dto.Amount);
            }
            return isUpdated;
        }

        public bool Delete(SalaryDetailDTO dto)
        {
            if (dto == null || dto.SalaryID <= 0) throw new ArgumentException("A valid SalaryID is required for deletion.", nameof(dto.SalaryID));
            return _salDao.Delete(new SALARY { ID = dto.SalaryID });
        }

        public SalaryDTO Select()
        {
            return new SalaryDTO
            {
                Salaries = _salDao.Select(),
                Employees = _empDao.Select(),
                Departments = _deptDao.Select(),
                Positions = _posDao.Select(),
                Months = _salDao.GetMonths()
            };
        }

        public bool GetBack(SalaryDetailDTO dto)
        {
            throw new NotSupportedException("Restoring deleted salary records is not supported.");
        }
    }
}