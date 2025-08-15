// File: CategoryExpensesDAO.cs
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DAO
{
    /// <summary>
    /// Data access for EXPENSE_CATEGORIES.
    /// </summary>
    public class CategoryExpensesDAO : StockContext, IDAO<EXPENSE_CATEGORIES, CategoryExpensesDetailDTO>
    {
        public bool Insert(EXPENSE_CATEGORIES entity)
        {
            try
            {
                DbContext.EXPENSE_CATEGORIES.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                // Handle validation errors
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");
                throw new Exception("Insert category failed:\n" + string.Join("\n", errorMessages));
            }
            catch (Exception ex)
            {
                // Handle other errors
                throw new Exception("Insert category failed.", ex);
            }
        }

        public bool Update(EXPENSE_CATEGORIES entity)
        {
            try
            {
                var dbEntity = DbContext.EXPENSE_CATEGORIES.First(c => c.ID == entity.ID);
                dbEntity.CategoryName = entity.CategoryName;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Update category failed.", ex);
            }
        }

        public bool Delete(EXPENSE_CATEGORIES entity)
        {
            try
            {
                // Using hard delete for categories for this example.
                // Could be changed to soft delete (IsActive = false).
                var dbEntity = DbContext.EXPENSE_CATEGORIES.First(x => x.ID == entity.ID);
                DbContext.EXPENSE_CATEGORIES.Remove(dbEntity);
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Delete category failed.", ex);
            }
        }

        public bool GetBack(int id)
        {
            throw new NotImplementedException();
        }

        public List<CategoryExpensesDetailDTO> Select()
        {
            return DbContext.EXPENSE_CATEGORIES
                .AsNoTracking()
                .Where(c => c.IsActive) // Only select active categories
                .Select(c => new CategoryExpensesDetailDTO
                {
                    ID = c.ID,
                    CategoryName = c.CategoryName
                })
                .OrderBy(c => c.CategoryName)
                .ToList();
        }
    }
}