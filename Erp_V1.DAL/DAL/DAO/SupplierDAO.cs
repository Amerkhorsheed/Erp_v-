using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DAO
{
    public class SupplierDAO : StockContext, IDAO<SUPPLIER, SupplierDetailDTO>
    {
        public virtual bool Insert(SUPPLIER entity)
        {
            try
            {
                DbContext.SUPPLIER.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Supplier insertion failed:\n{string.Join("\n", errorMessages)}", ex);
            }
        }

        public virtual bool Update(SUPPLIER entity)
        {
            try
            {
                var supplier = DbContext.SUPPLIER.FirstOrDefault(x => x.ID == entity.ID);
                if (supplier == null) return false;

                supplier.SupplierName = entity.SupplierName;
                supplier.PhoneNumber = entity.PhoneNumber;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                                       .SelectMany(e => e.ValidationErrors)
                                       .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Supplier update failed:\n{string.Join("\n", errorMessages)}", ex);
            }
        }

        public virtual bool Delete(SUPPLIER entity)
        {
            try
            {
                var supplier = DbContext.SUPPLIER.FirstOrDefault(x => x.ID == entity.ID);
                if (supplier == null) return false;

                supplier.isDeleted = true;
                supplier.DeletedDate = DateTime.Today;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Supplier deletion failed", ex);
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                var supplier = DbContext.SUPPLIER.FirstOrDefault(x => x.ID == ID);
                if (supplier == null) return false;

                supplier.isDeleted = false;
                supplier.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Supplier restoration failed", ex);
            }
        }

        public virtual List<SupplierDetailDTO> Select()
        {
            return Select(isDeleted: false);
        }

        public virtual List<SupplierDetailDTO> Select(bool isDeleted)
        {
            try
            {
                return DbContext.SUPPLIER
                    .Where(s => s.isDeleted == isDeleted)
                    .OrderBy(s => s.SupplierName)
                    .Select(s => new SupplierDetailDTO
                    {
                        SupplierID = s.ID,
                        SupplierName = s.SupplierName,
                        PhoneNumber = s.PhoneNumber,
                        isDeleted = s.isDeleted,
                        DeletedDate = s.DeletedDate
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Supplier retrieval failed", ex);
            }
        }
    }
}
