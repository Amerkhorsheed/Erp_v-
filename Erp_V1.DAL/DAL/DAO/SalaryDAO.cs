// File: SalaryDAO.cs
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DAO
{
    public class SalaryDAO : StockContext, IDAO<SALARY, SalaryDetailDTO>
    {
        public bool Insert(SALARY entity)
        {
            try
            {
                DbContext.SALARY.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors.SelectMany(e => e.ValidationErrors).Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Salary insertion failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public bool Update(SALARY entity)
        {
            try
            {
                var sal = DbContext.SALARY.First(x => x.ID == entity.ID);
                sal.Amount = entity.Amount;
                sal.Year = entity.Year;
                sal.MonthID = entity.MonthID;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors.SelectMany(e => e.ValidationErrors).Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Salary update failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public List<SalaryDetailDTO> Select() => Select(false);

        public List<SalaryDetailDTO> Select(bool includeDeleted)
        {
            // ** FIX: Switched to LEFT JOINs for robustness **
            var q = from s in DbContext.SALARY
                    join e in DbContext.EMPLOYEE on s.EmployeeID equals e.ID
                    join m in DbContext.MONTH on s.MonthID equals m.ID
                    join d_join in DbContext.DEPARTMENT on e.DepartmentID equals d_join.ID into d_group
                    from d in d_group.DefaultIfEmpty()
                    join p_join in DbContext.POSITION on e.PositionID equals p_join.ID into p_group
                    from p in p_group.DefaultIfEmpty()
                    orderby s.Year descending, s.MonthID descending
                    select new SalaryDetailDTO
                    {
                        SalaryID = s.ID,
                        EmployeeID = e.ID,
                        UserNo = e.UserNo,
                        Name = e.Name,
                        Surname = e.Surname,
                        DepartmentID = e.DepartmentID,
                        DepartmentName = d != null ? d.DepartmentName : "N/A",
                        PositionID = e.PositionID,
                        PositionName = p != null ? p.PositionName : "N/A",
                        MonthID = m.ID,
                        MonthName = m.MonthName,
                        Year = s.Year,
                        Amount = s.Amount,
                    };

            return q.ToList();
        }

        // ** NEW FEATURE METHOD **
        /// <summary>
        /// Checks if a salary record already exists for the given employee, month, and year.
        /// </summary>
        public bool Exists(int employeeId, int monthId, int year)
        {
            return DbContext.SALARY.Any(s => s.EmployeeID == employeeId && s.MonthID == monthId && s.Year == year);
        }

        #region Unchanged Methods
        public bool Delete(SALARY entity)
        {
            try
            {
                var sal = DbContext.SALARY.First(x => x.ID == entity.ID);
                DbContext.SALARY.Remove(sal);
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Salary deletion failed.", ex);
            }
        }

        public bool GetBack(int id)
        {
            throw new NotSupportedException("Restoring deleted salary records is not supported.");
        }

        public List<MONTH> GetMonths()
        {
            return DbContext.MONTH.OrderBy(m => m.ID).ToList();
        }
        #endregion
    }
}