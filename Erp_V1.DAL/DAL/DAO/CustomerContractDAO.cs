// File: CustomerContractDAO.cs
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;               // For ToListAsync, FirstAsync
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DAO
{
    public class CustomerContractDAO : StockContext
    {
        public async Task<bool> InsertAsync(CUSTOMERCONTRACT entity)
        {
            try
            {
                DbContext.CUSTOMERCONTRACT.Add(entity);
                return await DbContext.SaveChangesAsync() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var allErrors = ex.EntityValidationErrors
                                  .SelectMany(v => v.ValidationErrors)
                                  .Select(err => $"{err.PropertyName}: {err.ErrorMessage}");
                throw new Exception("Contract insertion failed:\n" + string.Join("\n", allErrors), ex);
            }
        }

        // File: CustomerContractDAO.cs
        public async Task<bool> UpdateAsync(CUSTOMERCONTRACT updated)
        {
            try
            {
                var db = await DbContext.CUSTOMERCONTRACT
                                        .FirstAsync(x => x.ID == updated.ID);

                // Map only the mutable fields
                db.CustomerID = updated.CustomerID;
                db.ContractNumber = updated.ContractNumber;
                db.Description = updated.Description;
                db.StartDate = updated.StartDate;
                db.EndDate = updated.EndDate;
                db.Status = updated.Status;
                // ← do NOT touch CreatedDate or DeletedDate

                return await DbContext.SaveChangesAsync() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors
                               .SelectMany(e => e.ValidationErrors)
                               .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Contract update failed:\n{string.Join("\n", errors)}", ex);
            }
        }


        public async Task<bool> DeleteAsync(CUSTOMERCONTRACT entity)
        {
            var dbEntity = await DbContext.CUSTOMERCONTRACT.FirstAsync(x => x.ID == entity.ID);
            dbEntity.IsDeleted = true;
            dbEntity.DeletedDate = DateTime.Now;
            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> GetBackAsync(int id)
        {
            var dbEntity = await DbContext.CUSTOMERCONTRACT.FirstAsync(x => x.ID == id);
            dbEntity.IsDeleted = false;
            dbEntity.DeletedDate = null;
            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<CustomerContractDTO>> SelectAsync()
        {
            return await (
                from c in DbContext.CUSTOMERCONTRACT
                join cust in DbContext.CUSTOMER on c.CustomerID equals cust.ID
                // only non-deleted contracts
                where !c.IsDeleted
                select new CustomerContractDTO
                {
                    ID = c.ID,
                    CustomerID = c.CustomerID,
                    CustomerName = cust.CustomerName,
                    ContractNumber = c.ContractNumber,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Status = c.Status,
                    IsDeleted = c.IsDeleted,
                    DeletedDate = c.DeletedDate
                }
            ).ToListAsync();
        }
    }
}
