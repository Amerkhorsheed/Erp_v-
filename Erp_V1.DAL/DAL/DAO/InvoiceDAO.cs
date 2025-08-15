using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Erp_V1.DAL.DAO
{
    public class InvoiceDAO : StockContext, IDAO<INVOICE, InvoiceDetailDTO>
    {
        #region Database Operations

        public virtual bool Delete(INVOICE entity)
        {
            try
            {
                if (entity.ID != 0)
                {
                    var invoice = DbContext.INVOICE.First(x => x.ID == entity.ID);
                    invoice.isDeleted = true;
                    invoice.DeletedDate = DateTime.Today;
                    invoice.ModifiedDate = DateTime.Now;
                    invoice.ModifiedBy = "System";
                    
                    // Also mark invoice items as deleted
                    var invoiceItems = DbContext.INVOICE_ITEM.Where(x => x.InvoiceID == entity.ID).ToList();
                    invoiceItems.ForEach(item =>
                    {
                        item.isDeleted = true;
                        item.DeletedDate = DateTime.Today;
                    });
                }
                else if (entity.CustomerID != 0)
                {
                    var invoices = DbContext.INVOICE.Where(x => x.CustomerID == entity.CustomerID).ToList();
                    invoices.ForEach(invoice =>
                    {
                        invoice.isDeleted = true;
                        invoice.DeletedDate = DateTime.Today;
                        invoice.ModifiedDate = DateTime.Now;
                        invoice.ModifiedBy = "System";
                        
                        // Also mark invoice items as deleted
                        var invoiceItems = DbContext.INVOICE_ITEM.Where(x => x.InvoiceID == invoice.ID).ToList();
                        invoiceItems.ForEach(item =>
                        {
                            item.isDeleted = true;
                            item.DeletedDate = DateTime.Today;
                        });
                    });
                }
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Invoice deletion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Invoice deletion failed", ex);
            }
        }

        public virtual bool Insert(INVOICE entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                entity.InvoiceState = entity.InvoiceState ?? "Draft";
                entity.CreatedBy = entity.CreatedBy ?? "System";
                
                DbContext.INVOICE.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Invoice insertion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Invoice insertion failed", ex);
            }
        }

        public virtual List<InvoiceDetailDTO> Select()
        {
            return ExecuteInvoiceQuery(false);
        }

        public virtual List<InvoiceDetailDTO> Select(bool isDeleted)
        {
            return ExecuteInvoiceQuery(isDeleted);
        }

        public virtual bool GetBack(int id)
        {
            try
            {
                var invoice = DbContext.INVOICE.FirstOrDefault(x => x.ID == id && x.isDeleted);
                if (invoice != null)
                {
                    invoice.isDeleted = false;
                    invoice.DeletedDate = null;
                    invoice.ModifiedDate = DateTime.Now;
                    invoice.ModifiedBy = "System";
                    return DbContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to restore invoice", ex);
            }
        }

        public virtual bool Update(INVOICE entity)
        {
            try
            {
                var invoice = DbContext.INVOICE.FirstOrDefault(x => x.ID == entity.ID);
                if (invoice == null)
                    return false;

                invoice.CustomerID = entity.CustomerID;
                invoice.InvoiceDate = entity.InvoiceDate;
                invoice.DueDate = entity.DueDate;
                invoice.SubTotal = entity.SubTotal;
                invoice.TaxAmount = entity.TaxAmount;
                invoice.DiscountAmount = entity.DiscountAmount;
                invoice.TotalAmount = entity.TotalAmount;
                invoice.PaidAmount = entity.PaidAmount;
                invoice.RemainingAmount = entity.RemainingAmount;
                invoice.InvoiceState = entity.InvoiceState;
                invoice.Notes = entity.Notes;
                invoice.ModifiedDate = DateTime.Now;
                invoice.ModifiedBy = entity.ModifiedBy ?? "System";

                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Invoice update failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Invoice update failed", ex);
            }
        }

        #endregion

        #region Helper Methods

        private List<InvoiceDetailDTO> ExecuteInvoiceQuery(bool isDeleted)
        {
            try
            {
                var query = from i in DbContext.INVOICE.Where(x => x.isDeleted == isDeleted)
                            join c in DbContext.CUSTOMER on i.CustomerID equals c.ID
                            select new InvoiceDetailDTO
                            {
                                InvoiceID = i.ID,
                                InvoiceNumber = i.InvoiceNumber,
                                CustomerID = i.CustomerID,
                                CustomerName = c.CustomerName,
                                CustomerEmail = c.Email,
                                CustomerPhone = c.Cust_Phone,
                                CustomerAddress = c.Cust_Address,
                                InvoiceDate = i.InvoiceDate,
                                DueDate = i.DueDate ?? DateTime.Today,
                                SubTotal = i.SubTotal,
                                TaxAmount = i.TaxAmount,
                                DiscountAmount = i.DiscountAmount,
                                TotalAmount = i.TotalAmount,
                                PaidAmount = i.PaidAmount,
                                RemainingAmount = i.RemainingAmount,
                                InvoiceState = i.InvoiceState,
                                Notes = i.Notes,
                                CreatedDate = i.CreatedDate,
                                ModifiedDate = i.ModifiedDate,
                                CreatedBy = i.CreatedBy,
                                ModifiedBy = i.ModifiedBy,
                                isDeleted = i.isDeleted,
                                isCustomerDeleted = c.isDeleted
                            };

                var invoices = query.ToList();

                // Load invoice items for each invoice
                foreach (var invoice in invoices)
                {
                    invoice.Items = GetInvoiceItems(invoice.InvoiceID);
                }

                return invoices;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve invoices", ex);
            }
        }

        private List<InvoiceItemDetailDTO> GetInvoiceItems(int invoiceId)
        {
            try
            {
                var query = from ii in DbContext.INVOICE_ITEM.Where(x => x.InvoiceID == invoiceId && !x.isDeleted)
                            join p in DbContext.PRODUCT on ii.ProductID equals p.ID
                            join cat in DbContext.CATEGORY on ii.CategoryID equals cat.ID
                            select new InvoiceItemDetailDTO
                            {
                                ItemID = ii.ID,
                                InvoiceID = ii.InvoiceID,
                                ProductID = ii.ProductID,
                                ProductName = p.ProductName,
                                CategoryID = ii.CategoryID,
                                CategoryName = cat.CategoryName,
                                Description = ii.Description,
                                Quantity = ii.Quantity,
                                UnitPrice = ii.UnitPrice,
                                DiscountPercentage = ii.DiscountPercentage,
                                DiscountAmount = ii.DiscountAmount,
                                LineTotal = ii.LineTotal,
                                isDeleted = ii.isDeleted,
                                isProductDeleted = p.isDeleted,
                                isCategoryDeleted = cat.isDeleted,
                                StockAmount = p.StockAmount,
                                MinQty = (int)(p.MinQty ?? 0),
                                MaxDiscount = (int)(p.MaxDiscount ?? 0)
                            };

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve invoice items for invoice {invoiceId}", ex);
            }
        }

        public virtual bool UpdateInvoiceState(int invoiceId, string newState)
        {
            try
            {
                var invoice = DbContext.INVOICE.FirstOrDefault(x => x.ID == invoiceId);
                if (invoice == null)
                    return false;

                invoice.InvoiceState = newState;
                invoice.ModifiedDate = DateTime.Now;
                invoice.ModifiedBy = "System";

                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update invoice state for invoice {invoiceId}", ex);
            }
        }

        #endregion
    }
}