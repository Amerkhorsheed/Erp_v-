// File: ExpensesBLL.cs
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Business logic for managing expenses: validation, defaults, workflow.
    /// </summary>
    public class ExpensesBLL : IBLL<ExpensesDetailDTO, ExpensesDTO>
    {
        private readonly ExpensesDAO _dao = new ExpensesDAO();

        public bool Insert(ExpensesDetailDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.ExpensesName)) throw new ArgumentException("Name required.");
            if (dto.Amount <= 0) throw new ArgumentException("Amount > 0.");
            if (dto.CategoryID <= 0) throw new ArgumentException("Valid category required.");

            var e = new EXPENSES
            {
                ExpensesName = dto.ExpensesName,
                CategoryID = dto.CategoryID,
                ExpenseDate = dto.ExpenseDate.Date,
                Amount = dto.Amount,
                CurrencyCode = dto.CurrencyCode,
                Note = dto.Note,
                Status = "Pending", // Status is always Pending on insert
                RequestedBy = dto.RequestedBy,
                RequestedDate = DateTime.UtcNow,
                AttachmentPath = dto.AttachmentPath,
                CreatedBy = dto.CreatedBy,
                CreatedDate = DateTime.UtcNow
            };
            return _dao.Insert(e);
        }

        public bool Update(ExpensesDetailDTO dto)
        {
            if (dto == null || dto.ID <= 0) throw new ArgumentException("Valid ID required.");
            if (dto.ModifiedBy <= 0) throw new ArgumentException("ModifiedBy required.");

            var e = new EXPENSES
            {
                ID = dto.ID,
                ExpensesName = dto.ExpensesName,
                CategoryID = dto.CategoryID,
                ExpenseDate = dto.ExpenseDate.Date,
                Amount = dto.Amount,
                CurrencyCode = dto.CurrencyCode,
                Note = dto.Note,
                Status = dto.Status, // Status is carried over
                AttachmentPath = dto.AttachmentPath,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = DateTime.UtcNow
            };
            return _dao.Update(e);
        }

        /// <summary>
        /// Updates only the status of an expense (for approval workflows).
        /// </summary>
        public bool UpdateStatus(int expenseId, string newStatus, int approvedBy)
        {
            if (expenseId <= 0) throw new ArgumentException("Valid expense ID is required.");
            if (string.IsNullOrWhiteSpace(newStatus)) throw new ArgumentException("New status is required.");

            return _dao.UpdateStatus(expenseId, newStatus, approvedBy);
        }

        public bool Delete(ExpensesDetailDTO dto)
        {
            if (dto == null || dto.ID <= 0) throw new ArgumentException("Valid ID required.");
            var e = new EXPENSES { ID = dto.ID, DeletedBy = dto.DeletedBy };
            return _dao.Delete(e);
        }

        public bool GetBack(ExpensesDetailDTO dto)
        {
            if (dto == null || dto.ID <= 0) throw new ArgumentException("Valid ID required.");
            return _dao.GetBack(dto.ID);
        }

        public ExpensesDTO Select()
        {
            var dto = new ExpensesDTO();
            dto.Expenses = _dao.Select();
            dto.Categories = new CategoryExpensesDAO().Select(); // Use the dedicated DAO
            dto.CurrencyCodes = new List<string> { "USD", "EUR", "GBP", "SY" };
            dto.StatusList = new List<string> { "Pending", "Approved", "Rejected" };
            return dto;
        }
    }
}