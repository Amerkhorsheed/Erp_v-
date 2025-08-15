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
    public class InvoiceItemDAO : StockContext, IDAO<INVOICE_ITEM, InvoiceItemDetailDTO>
    {
        #region Database Operations

        public virtual bool Delete(INVOICE_ITEM entity)
        {
            try
            {
                if (entity.ID != 0)
                {
                    var item = DbContext.INVOICE_ITEM.First(x => x.ID == entity.ID);
                    item.isDeleted = true;
                    item.DeletedDate = DateTime.Today;
                }
                else if (entity.InvoiceID != 0)
                {
                    var items = DbContext.INVOICE_ITEM.Where(x => x.InvoiceID == entity.InvoiceID).ToList();
                    items.ForEach(item =>
                    {
                        item.isDeleted = true;
                        item.DeletedDate = DateTime.Today;
                    });
                }
                else if (entity.ProductID != 0)
                {
                    var items = DbContext.INVOICE_ITEM.Where(x => x.ProductID == entity.ProductID).ToList();
                    items.ForEach(item =>
                    {
                        item.isDeleted = true;
                        item.DeletedDate = DateTime.Today;
                    });
                }
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Invoice item deletion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Invoice item deletion failed", ex);
            }
        }

        public virtual bool Insert(INVOICE_ITEM entity)
        {
            try
            {
                DbContext.INVOICE_ITEM.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Invoice item insertion failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Invoice item insertion failed", ex);
            }
        }

        public virtual List<InvoiceItemDetailDTO> Select()
        {
            return ExecuteInvoiceItemQuery(false);
        }

        public virtual List<InvoiceItemDetailDTO> Select(bool isDeleted)
        {
            return ExecuteInvoiceItemQuery(isDeleted);
        }

        public virtual List<InvoiceItemDetailDTO> SelectByInvoiceId(int invoiceId)
        {
            return ExecuteInvoiceItemQuery(false, invoiceId);
        }

        public virtual bool GetBack(int id)
        {
            try
            {
                var item = DbContext.INVOICE_ITEM.FirstOrDefault(x => x.ID == id && x.isDeleted);
                if (item != null)
                {
                    item.isDeleted = false;
                    item.DeletedDate = null;
                    return DbContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to restore invoice item", ex);
            }
        }

        public virtual bool Update(INVOICE_ITEM entity)
        {
            try
            {
                var item = DbContext.INVOICE_ITEM.FirstOrDefault(x => x.ID == entity.ID);
                if (item == null)
                    return false;

                item.ProductID = entity.ProductID;
                item.CategoryID = entity.CategoryID;
                item.ProductName = entity.ProductName;
                item.Description = entity.Description;
                item.Quantity = entity.Quantity;
                item.UnitPrice = entity.UnitPrice;
                item.DiscountPercentage = entity.DiscountPercentage;
                item.DiscountAmount = entity.DiscountAmount;
                item.LineTotal = entity.LineTotal;

                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Invoice item update failed. Validation errors:\n{string.Join("\n", errorMessages)}");
            }
            catch (Exception ex)
            {
                throw new Exception("Invoice item update failed", ex);
            }
        }

        #endregion

        #region Helper Methods

        private List<InvoiceItemDetailDTO> ExecuteInvoiceItemQuery(bool isDeleted, int? invoiceId = null)
        {
            try
            {
                var itemsQuery = DbContext.INVOICE_ITEM.Where(x => x.isDeleted == isDeleted);
                
                if (invoiceId.HasValue)
                {
                    itemsQuery = itemsQuery.Where(x => x.InvoiceID == invoiceId.Value);
                }

                var query = from ii in itemsQuery
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
                throw new Exception("Failed to retrieve invoice items", ex);
            }
        }

        #endregion
    }
}