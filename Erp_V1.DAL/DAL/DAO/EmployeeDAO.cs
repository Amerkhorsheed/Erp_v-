// File: EmployeeDAO.cs
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DAO
{
    public class EmployeeDAO : StockContext, IDAO<EMPLOYEE, EmployeeDetailDTO>
    {
        // ... (Insert, Delete, GetBack, Update, and other methods are unchanged) ...
        #region Unchanged Methods
        public bool Insert(EMPLOYEE e)
        {
            try
            {
                DbContext.EMPLOYEE.Add(e);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                             .SelectMany(x => x.ValidationErrors)
                             .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Employee insert failed:\n{string.Join("\n", errs)}", ex);
            }
        }

        public bool Delete(EMPLOYEE e)
        {
            try
            {
                var emp = DbContext.EMPLOYEE.First(x => x.ID == e.ID);
                emp.IsDeleted = true;
                emp.DeletedDate = DateTime.UtcNow;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Employee deletion failed.", ex);
            }
        }

        public bool GetBack(int id)
        {
            try
            {
                var emp = DbContext.EMPLOYEE.First(x => x.ID == id);
                emp.IsDeleted = false;
                emp.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Employee restoration failed.", ex);
            }
        }

        public bool Update(EMPLOYEE e)
        {
            try
            {
                var emp = DbContext.EMPLOYEE.First(x => x.ID == e.ID);
                emp.UserNo = e.UserNo;
                emp.Name = e.Name;
                emp.Surname = e.Surname;
                emp.Password = e.Password;
                emp.BirthDay = e.BirthDay;
                emp.Address = e.Address;
                emp.ImagePath = e.ImagePath;
                emp.Salary = e.Salary;
                emp.DepartmentID = e.DepartmentID;
                emp.PositionID = e.PositionID;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errs = ex.EntityValidationErrors
                             .SelectMany(x => x.ValidationErrors)
                             .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception($"Employee update failed:\n{string.Join("\n", errs)}", ex);
            }
        }
        public bool AssignRole(int employeeId, int roleId)
        {
            try
            {
                var emp = DbContext.EMPLOYEE
                                   .Include(e => e.ROLE)
                                   .First(e => e.ID == employeeId);
                emp.ROLE.Clear();
                var role = DbContext.ROLE.Find(roleId);
                if (role != null)
                {
                    emp.ROLE.Add(role);
                }
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Role assignment failed.", ex);
            }
        }
        public bool UpdateSalary(int employeeId, int newSalary)
        {
            try
            {
                var emp = DbContext.EMPLOYEE.First(e => e.ID == employeeId);
                emp.Salary = newSalary;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Employee salary update failed.", ex);
            }
        }
        public string GetPasswordHash(int employeeId)
        {
            return DbContext.EMPLOYEE
                .Where(e => e.ID == employeeId)
                .Select(e => e.Password)
                .FirstOrDefault();
        }
        #endregion

        public List<EmployeeDetailDTO> Select() => Select(false);

        public List<EmployeeDetailDTO> Select(bool includeDeleted)
        {
            // ** THIS QUERY IS NOW MORE ROBUST **
            var q = from e in DbContext.EMPLOYEE
                    where includeDeleted || !e.IsDeleted
                    // Use LEFT JOINs for Department and Position
                    join d_join in DbContext.DEPARTMENT on e.DepartmentID equals d_join.ID into d_group
                    from d in d_group.DefaultIfEmpty()
                    join p_join in DbContext.POSITION on e.PositionID equals p_join.ID into p_group
                    from p in p_group.DefaultIfEmpty()
                        // Role was already a LEFT JOIN, which is correct
                    from ri in e.ROLE.DefaultIfEmpty()
                    orderby e.UserNo
                    select new EmployeeDetailDTO
                    {
                        EmployeeID = e.ID,
                        UserNo = e.UserNo,
                        Name = e.Name,
                        Surname = e.Surname,
                        // Safely handle potentially null values
                        DepartmentID = d != null ? d.ID : 0,
                        DepartmentName = d != null ? d.DepartmentName : "N/A",
                        PositionID = p != null ? p.ID : 0,
                        PositionName = p != null ? p.PositionName : "N/A",
                        Salary = e.Salary,
                        Password = e.Password,
                        ImagePath = e.ImagePath,
                        Address = e.Address,
                        BirthDay = e.BirthDay,
                        RoleID = ri != null ? ri.ID : 0,
                        RoleName = ri != null ? ri.RoleName : string.Empty
                    };
            return q.ToList();
        }
        public EmployeeDetailDTO GetByUserNo(int userNo)
        {
            // Use the existing robust Select method
            return this.Select(false).FirstOrDefault(e => e.UserNo == userNo);
        }
    }
}