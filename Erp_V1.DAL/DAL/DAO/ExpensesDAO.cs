// File: ExpensesDAO.cs
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DAO
{
    public class ExpensesDAO : StockContext, IDAO<EXPENSES, ExpensesDetailDTO>
    {
        // ... Insert method and others are unchanged ...
        #region Unchanged Methods
        public List<CategoryExpensesDetailDTO> SelectCategories()
        {
            return new CategoryExpensesDAO().Select();
        }

        public bool Insert(EXPENSES e)
        {
            try
            {
                DbContext.EXPENSES.Add(e);
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Insert Expense failed", ex);
            }
        }
        #endregion

        // THIS METHOD IS NOW CORRECTED
        public bool Update(EXPENSES e)
        {
            try
            {
                var db = DbContext.EXPENSES.First(x => x.ID == e.ID);
                db.ExpensesName = e.ExpensesName;
                db.CategoryID = e.CategoryID;
                db.ExpenseDate = e.ExpenseDate;
                db.Amount = e.Amount;
                db.CurrencyCode = e.CurrencyCode;
                db.Note = e.Note;
                // ** THIS LINE IS NEW **: Allow status to be updated from the edit form.
                db.Status = e.Status;
                db.AttachmentPath = e.AttachmentPath;
                db.ModifiedBy = e.ModifiedBy;
                db.ModifiedDate = DateTime.UtcNow;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Update Expense failed", ex);
            }
        }

        // The rest of the file (UpdateStatus, Delete, GetBack, Select) remains the same
        #region Unchanged Methods
        public bool UpdateStatus(int expenseId, string newStatus, int approvedBy)
        {
            try
            {
                var db = DbContext.EXPENSES.First(x => x.ID == expenseId);
                db.Status = newStatus;
                db.ApprovedBy = approvedBy;
                db.ApprovedDate = DateTime.UtcNow;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Update Expense Status failed", ex);
            }
        }

        public bool Delete(EXPENSES e)
        {
            try
            {
                var db = DbContext.EXPENSES.First(x => x.ID == e.ID);
                db.IsDeleted = true;
                db.DeletedBy = e.DeletedBy;
                db.DeletedDate = DateTime.UtcNow;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Expense failed", ex);
            }
        }

        public bool GetBack(int id)
        {
            try
            {
                var db = DbContext.EXPENSES.First(x => x.ID == id);
                db.IsDeleted = false;
                db.DeletedBy = null;
                db.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Restore Expense failed", ex);
            }
        }

        public List<ExpensesDetailDTO> Select()
        {
            var q = from e in DbContext.EXPENSES.Where(x => !x.IsDeleted)
                    join c in DbContext.EXPENSE_CATEGORIES on e.CategoryID equals c.ID into cj
                    from c in cj.DefaultIfEmpty()
                    join req in DbContext.EMPLOYEE on e.RequestedBy equals req.ID into reqj
                    from req in reqj.DefaultIfEmpty()
                    join apr in DbContext.EMPLOYEE on e.ApprovedBy equals apr.ID into aprj
                    from apr in aprj.DefaultIfEmpty()
                    select new ExpensesDetailDTO
                    {
                        ID = e.ID,
                        ExpensesName = e.ExpensesName,
                        CategoryID = e.CategoryID,
                        CategoryName = c != null ? c.CategoryName : "N/A",
                        ExpenseDate = e.ExpenseDate,
                        Amount = e.Amount,
                        CurrencyCode = e.CurrencyCode,
                        Note = e.Note,
                        Status = e.Status,
                        RequestedBy = e.RequestedBy,
                        RequestedByName = req != null ? req.Name + " " + req.Surname : "N/A",
                        RequestedDate = e.RequestedDate,
                        ApprovedBy = e.ApprovedBy,
                        ApprovedByName = apr != null ? apr.Name + " " + apr.Surname : null,
                        ApprovedDate = e.ApprovedDate,
                        ApprovalComment = e.ApprovalComment,
                        AttachmentPath = e.AttachmentPath,
                        IsDeleted = e.IsDeleted,
                        CreatedBy = e.CreatedBy,
                        CreatedDate = e.CreatedDate,
                        ModifiedBy = e.ModifiedBy,
                        ModifiedDate = e.ModifiedDate,
                        DeletedBy = e.DeletedBy,
                        DeletedDate = e.DeletedDate
                    };
            return q.OrderByDescending(x => x.ExpenseDate).ToList();
        }
        #endregion
    }
}