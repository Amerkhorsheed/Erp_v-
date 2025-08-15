// File: CategoryExpensesBLL.cs
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; // <--- FIX IS HERE
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Business logic for managing expense categories.
    /// </summary>
    public class CategoryExpensesBLL : IBLL<CategoryExpensesDetailDTO, object>
    {
        private readonly CategoryExpensesDAO _dao = new CategoryExpensesDAO();

        public bool Insert(CategoryExpensesDetailDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.CategoryName)) throw new ArgumentException("Category name is required.");

            var category = new EXPENSE_CATEGORIES
            {
                CategoryName = dto.CategoryName,
                IsActive = true // Default to active on creation
            };
            return _dao.Insert(category);
        }

        public async Task<bool> InsertAsync(CategoryExpensesDetailDTO dto)
        {
            return await Task.Run(() => Insert(dto));
        }

        public bool Update(CategoryExpensesDetailDTO dto)
        {
            if (dto == null || dto.ID <= 0) throw new ArgumentException("A valid category ID is required for an update.");
            if (string.IsNullOrWhiteSpace(dto.CategoryName)) throw new ArgumentException("Category name is required.");

            var category = new EXPENSE_CATEGORIES
            {
                ID = dto.ID,
                CategoryName = dto.CategoryName
            };
            return _dao.Update(category);
        }

        public async Task<bool> UpdateAsync(CategoryExpensesDetailDTO dto)
        {
            return await Task.Run(() => Update(dto));
        }

        public bool Delete(CategoryExpensesDetailDTO dto)
        {
            if (dto == null || dto.ID <= 0) throw new ArgumentException("A valid ID is required to delete.");
            var category = new EXPENSE_CATEGORIES { ID = dto.ID };
            return _dao.Delete(category);
        }

        public bool GetBack(CategoryExpensesDetailDTO dto)
        {
            throw new NotImplementedException("Restore functionality is not implemented for categories.");
        }

        public object Select()
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryExpensesDetailDTO>> SelectListAsync()
        {
            return await Task.Run(() => _dao.Select());
        }
    }
}