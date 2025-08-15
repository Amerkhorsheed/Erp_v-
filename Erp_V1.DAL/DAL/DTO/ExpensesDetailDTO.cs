// File: ExpensesDetailDTO.cs
using System;
using System.Collections.Generic;

namespace Erp_V1.DAL.DTO
{
    /// <summary>
    /// Detailed DTO for one expense, including all workflow and audit fields.
    /// </summary>
    public class ExpensesDetailDTO
    {
        public int ID { get; set; }
        public string ExpensesName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public int RequestedBy { get; set; }
        public string RequestedByName { get; set; }
        public DateTime RequestedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovalComment { get; set; }
        public string AttachmentPath { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }

    /// <summary>
    /// Container DTO for a list of expenses plus lookup lists.
    /// </summary>
    public class ExpensesDTO
    {
        public List<ExpensesDetailDTO> Expenses { get; set; }
        public List<CategoryExpensesDetailDTO> Categories { get; set; }
        public List<string> CurrencyCodes { get; set; }
        public List<string> StatusList { get; set; }
    }

    /// <summary>
    /// DTO for an expense category.
    /// </summary>
    public class CategoryExpensesDetailDTO
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
    }
}
