using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System.Linq;
using Erp_V1.DAL.DAL;
using System;
using System.Collections.Generic;

namespace Erp_V1.BLL
{
    public class InvoiceBLL : IBLL<InvoiceDetailDTO, InvoiceDTO>
    {
        #region Data Access Dependencies
        private readonly InvoiceDAO _invoiceDao = new InvoiceDAO();
        private readonly InvoiceItemDAO _invoiceItemDao = new InvoiceItemDAO();
        private readonly ProductDAO _productDao = new ProductDAO();
        private readonly CategoryDAO _categoryDao = new CategoryDAO();
        private readonly CustomerDAO _customerDao = new CustomerDAO();
        #endregion

        #region CRUD Operations

        public bool Delete(InvoiceDetailDTO entity)
        {
            var invoice = new INVOICE { ID = entity.InvoiceID };
            return _invoiceDao.Delete(invoice);
        }

        public bool Insert(InvoiceDetailDTO entity)
        {
            try
            {
                // Generate invoice number if not provided
                if (string.IsNullOrEmpty(entity.InvoiceNumber))
                {
                    entity.InvoiceNumber = GenerateInvoiceNumber();
                }

                // Calculate totals
                CalculateInvoiceTotals(entity);

                var invoice = new INVOICE
                {
                    InvoiceNumber = entity.InvoiceNumber,
                    CustomerID = entity.CustomerID,
                    InvoiceDate = entity.InvoiceDate,
                    DueDate = entity.DueDate,
                    SubTotal = entity.SubTotal,
                    TaxAmount = entity.TaxAmount,
                    DiscountAmount = entity.DiscountAmount,
                    TotalAmount = entity.TotalAmount,
                    PaidAmount = entity.PaidAmount,
                    RemainingAmount = entity.RemainingAmount,
                    InvoiceState = entity.InvoiceState ?? "Draft",
                    Notes = entity.Notes,
                    CreatedBy = entity.CreatedBy
                };

                if (_invoiceDao.Insert(invoice))
                {
                    // Get the generated invoice ID
                    int newInvoiceId = invoice.ID;
                    entity.InvoiceID = newInvoiceId;

                    // Insert invoice items
                    foreach (var item in entity.Items)
                    {
                        var invoiceItem = new INVOICE_ITEM
                        {
                            InvoiceID = newInvoiceId,
                            ProductID = item.ProductID,
                            CategoryID = item.CategoryID,
                            ProductName = item.ProductName,
                            Description = item.Description,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            DiscountPercentage = item.DiscountPercentage,
                            DiscountAmount = item.DiscountAmount,
                            LineTotal = item.LineTotal
                        };

                        _invoiceItemDao.Insert(invoiceItem);

                        // Update product stock if invoice is not in Draft state
                        if (entity.InvoiceState != "Draft")
                        {
                            UpdateProductStock(item.ProductID, item.Quantity, false);
                        }
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create invoice", ex);
            }
        }

        public virtual InvoiceDTO Select()
        {
            return new InvoiceDTO
            {
                Invoices = _invoiceDao.Select(),
                Customers = _customerDao.Select(),
                Products = _productDao.Select(),
                Categories = _categoryDao.Select(),
                InvoiceItems = _invoiceItemDao.Select()
            };
        }

        public bool Update(InvoiceDetailDTO entity)
        {
            try
            {
                // Calculate totals
                CalculateInvoiceTotals(entity);

                var invoice = new INVOICE
                {
                    ID = entity.InvoiceID,
                    CustomerID = entity.CustomerID,
                    InvoiceDate = entity.InvoiceDate,
                    DueDate = entity.DueDate,
                    SubTotal = entity.SubTotal,
                    TaxAmount = entity.TaxAmount,
                    DiscountAmount = entity.DiscountAmount,
                    TotalAmount = entity.TotalAmount,
                    PaidAmount = entity.PaidAmount,
                    RemainingAmount = entity.RemainingAmount,
                    InvoiceState = entity.InvoiceState,
                    Notes = entity.Notes,
                    ModifiedBy = entity.ModifiedBy
                };

                return _invoiceDao.Update(invoice);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update invoice", ex);
            }
        }

        #endregion

        #region Invoice State Management

        public bool UpdateInvoiceState(int invoiceId, string newState)
        {
            try
            {
                var invoice = _invoiceDao.Select().FirstOrDefault(i => i.InvoiceID == invoiceId);
                if (invoice == null)
                    return false;

                string oldState = invoice.InvoiceState;
                
                // Update the state
                bool result = _invoiceDao.UpdateInvoiceState(invoiceId, newState);
                
                if (result)
                {
                    // Handle stock adjustments based on state changes
                    HandleStateChangeStockAdjustments(invoice, oldState, newState);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update invoice state for invoice {invoiceId}", ex);
            }
        }

        public bool MarkAsPaid(int invoiceId, decimal paidAmount)
        {
            try
            {
                var invoice = _invoiceDao.Select().FirstOrDefault(i => i.InvoiceID == invoiceId);
                if (invoice == null)
                    return false;

                invoice.PaidAmount += paidAmount;
                invoice.RemainingAmount = invoice.TotalAmount - invoice.PaidAmount;
                
                // Update state based on payment
                if (invoice.RemainingAmount <= 0)
                {
                    invoice.InvoiceState = "Paid";
                }
                else if (invoice.PaidAmount > 0)
                {
                    invoice.InvoiceState = "Partial";
                }

                return Update(invoice);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to mark invoice {invoiceId} as paid", ex);
            }
        }

        #endregion

        #region Helper Methods

        private string GenerateInvoiceNumber()
        {
            var lastInvoice = _invoiceDao.Select().OrderByDescending(i => i.InvoiceID).FirstOrDefault();
            int nextNumber = 1;
            
            if (lastInvoice != null && !string.IsNullOrEmpty(lastInvoice.InvoiceNumber))
            {
                var parts = lastInvoice.InvoiceNumber.Split('-');
                if (parts.Length > 1 && int.TryParse(parts[1], out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"INV-{nextNumber:D6}";
        }

        private void CalculateInvoiceTotals(InvoiceDetailDTO invoice)
        {
            if (invoice.Items == null || !invoice.Items.Any())
            {
                invoice.SubTotal = 0;
                invoice.TotalAmount = 0;
                invoice.RemainingAmount = 0;
                return;
            }

            // Calculate line totals for each item
            foreach (var item in invoice.Items)
            {
                item.LineTotal = (item.UnitPrice * item.Quantity) - item.DiscountAmount;
            }

            // Calculate invoice totals
            invoice.SubTotal = invoice.Items.Sum(i => i.LineTotal);
            invoice.TotalAmount = invoice.SubTotal + invoice.TaxAmount - invoice.DiscountAmount;
            invoice.RemainingAmount = invoice.TotalAmount - invoice.PaidAmount;
        }

        private void UpdateProductStock(int productId, int quantity, bool isReturning)
        {
            try
            {
                var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == productId);
                if (productDTO != null)
                {
                    int stockAdjustment = isReturning ? quantity : -quantity;
                    var updatedStock = productDTO.stockAmount + stockAdjustment;
                    
                    var product = new PRODUCT
                    {
                        ID = productDTO.ProductID,
                        ProductName = productDTO.ProductName,
                        Price = productDTO.price,
                        StockAmount = updatedStock,
                        CategoryID = productDTO.CategoryID,
                        Sale_Price = productDTO.Sale_Price,
                        MinQty = productDTO.MinQty,
                        MaxDiscount = productDTO.MaxDiscount
                    };
                    _productDao.Update(product);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update stock for product {productId}", ex);
            }
        }

        private void HandleStateChangeStockAdjustments(InvoiceDetailDTO invoice, string oldState, string newState)
        {
            // Define states that affect stock
            var stockAffectingStates = new[] { "Confirmed", "Shipped", "Delivered", "Paid" };
            
            bool oldStateAffectsStock = stockAffectingStates.Contains(oldState);
            bool newStateAffectsStock = stockAffectingStates.Contains(newState);

            if (!oldStateAffectsStock && newStateAffectsStock)
            {
                // Moving from non-stock-affecting to stock-affecting state - reduce stock
                foreach (var item in invoice.Items)
                {
                    UpdateProductStock(item.ProductID, item.Quantity, false);
                }
            }
            else if (oldStateAffectsStock && !newStateAffectsStock)
            {
                // Moving from stock-affecting to non-stock-affecting state - restore stock
                foreach (var item in invoice.Items)
                {
                    UpdateProductStock(item.ProductID, item.Quantity, true);
                }
            }
        }

        public List<InvoiceDetailDTO> GetInvoicesByState(string state)
        {
            return _invoiceDao.Select().Where(i => i.InvoiceState == state).ToList();
        }

        public List<InvoiceDetailDTO> GetOverdueInvoices()
        {
            var today = DateTime.Today;
            return _invoiceDao.Select()
                .Where(i => i.DueDate < today && i.InvoiceState != "Paid" && i.InvoiceState != "Cancelled")
                .ToList();
        }

        public bool GetBack(InvoiceDetailDTO entity)
        {
            try
            {
                return _invoiceDao.GetBack(entity.InvoiceID);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to restore invoice", ex);
            }
        }

        #endregion
    }
}