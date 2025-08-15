using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System.Linq;
using Erp_V1.DAL.DAL;
using System; // Add this for Exception
using System.Collections.Generic;
using System.Data.Entity;

namespace Erp_V1.BLL
{
    public class SalesBLL : IBLL<SalesDetailDTO, SalesDTO>
    {
        #region Data Access Dependencies
        private readonly SalesDAO _salesDao = new SalesDAO();
        private readonly ProductDAO _productDao = new ProductDAO();
        private readonly CategoryDAO _categoryDao = new CategoryDAO();
        private readonly CustomerDAO _customerDao = new CustomerDAO();
        private readonly DeliveryBLL _deliveryBll = new DeliveryBLL();
        #endregion

        #region CRUD Operations

        public int? InsertAndReturnId(SalesDetailDTO entity)
        {
            return InsertAndReturnIdWithTransaction(entity, null);
        }

        public List<int> InsertTransactionWithItems(List<SalesDetailDTO> entities, string transactionId)
        {
            var savedSaleIds = new List<int>();
            
            try
            {
                foreach (var entity in entities)
                {
                    var saleId = InsertAndReturnIdWithTransaction(entity, transactionId);
                    if (saleId.HasValue)
                    {
                        savedSaleIds.Add(saleId.Value);
                    }
                    else
                    {
                        throw new Exception($"Failed to save sale record for Product ID {entity.ProductID}.");
                    }
                }
                
                return savedSaleIds;
            }
            catch
            {
                // If any item fails, we could implement rollback logic here
                // For now, we'll let the exception bubble up
                throw;
            }
        }

        public int? InsertAndReturnIdWithTransaction(SalesDetailDTO entity, string transactionId)
        {
            var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == entity.ProductID);
            if (productDTO == null)
            {
                throw new Exception($"Product with ID {entity.ProductID} not found.");
            }

            if (productDTO.stockAmount < entity.SalesAmount)
            {
                throw new Exception($"Not enough stock for product '{productDTO.ProductName}'. Requested: {entity.SalesAmount}, Available: {productDTO.stockAmount}");
            }

            // Calculate total amount for this item
            var itemTotal = (entity.Price * entity.SalesAmount) - (int)entity.MaxDiscount;
            
            var sales = new SALES
            {
                CategoryID = productDTO.CategoryID,
                ProductID = entity.ProductID,
                CustomerID = entity.CustomerID,
                ProductSalesPrice = entity.Price,
                ProductSalesAmout = entity.SalesAmount,
                SalesDate = entity.SalesDate,
                MaxDiscount = entity.MaxDiscount,
                TransactionId = transactionId,
                Total = itemTotal,
                Madfou3 = entity.Madfou3,
                Baky = entity.Baky,
                isDeleted = false
            };

            if (_salesDao.Insert(sales))
            {
                int newSaleId = sales.ID;

                var productToUpdate = new PRODUCT
                {
                    ID = productDTO.ProductID,
                    StockAmount = productDTO.stockAmount - entity.SalesAmount
                };
                // It is better to have a dedicated method for updating only stock.
                // Assuming you have added UpdateStock(productToUpdate) to your ProductDAO.
                _productDao.UpdateStock(productToUpdate);

                // --- FIX IS HERE ---
                var deliveryForSale = new DeliveryDetailDTO
                {
                    SalesID = newSaleId,
                    // --- FIX #1: Use the correct property name ---
                    AssignedDate = DateTime.Now,
                    // --- FIX #2: Set a default status for the new delivery ---
                    Status = "Pending"
                };
                _deliveryBll.Insert(deliveryForSale);

                return newSaleId;
            }

            return null;
        }

        public bool Insert(SalesDetailDTO entity)
        {
            return InsertAndReturnId(entity).HasValue;
        }

        public bool Delete(SalesDetailDTO entity)
        {
            var sales = new SALES { ID = entity.SalesID };
            _salesDao.Delete(sales);

            var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == entity.ProductID);
            if (productDTO != null)
            {
                var product = new PRODUCT
                {
                    ID = productDTO.ProductID,
                    StockAmount = productDTO.stockAmount + entity.SalesAmount,
                };
                _productDao.UpdateStock(product);
            }

            return true;
        }

        public bool GetBack(SalesDetailDTO entity)
        {
            _salesDao.GetBack(entity.SalesID);

            var productDTO = _productDao.Select().FirstOrDefault(p => p.ProductID == entity.ProductID);
            if (productDTO != null)
            {
                var product = new PRODUCT
                {
                    ID = productDTO.ProductID,
                    StockAmount = productDTO.stockAmount - entity.SalesAmount,
                };
                _productDao.UpdateStock(product);
            }

            return true;
        }

        public virtual SalesDTO Select()
        {
            return new SalesDTO
            {
                Products = _productDao.Select(),
                Customers = _customerDao.Select(),
                Categories = _categoryDao.Select(),
                Sales = _salesDao.Select()
            };
        }

        public bool Update(SalesDetailDTO entity)
        {
            var sales = new SALES
            {
                ID = entity.SalesID,
                ProductSalesAmout = entity.SalesAmount,
                ProductSalesPrice = entity.Price,
                SalesDate = entity.SalesDate,
                MaxDiscount = entity.MaxDiscount,
            };
            return _salesDao.Update(sales);
        }
        #endregion

        #region Filtered Retrieval
        public virtual SalesDTO Select(bool isDeleted)
        {
            return new SalesDTO
            {
                Products = _productDao.Select(isDeleted),
                Customers = _customerDao.Select(isDeleted),
                Categories = _categoryDao.Select(isDeleted),
                Sales = _salesDao.Select(isDeleted)
            };
        }
        #endregion
    }
}