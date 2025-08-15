using System;
using System.Collections.Generic;

namespace Erp_V1.DAL.DTO
{
    public class InvoiceDetailDTO
    {
        public int InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public string InvoiceState { get; set; } // Draft, Pending, Paid, Overdue, Cancelled
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool isDeleted { get; set; }
        public bool isCustomerDeleted { get; set; }
        
        // Invoice Items
        public List<InvoiceItemDetailDTO> Items { get; set; }
        
        public InvoiceDetailDTO()
        {
            Items = new List<InvoiceItemDetailDTO>();
        }
    }
}