// Erp_V1.DAL.DAO/SupplierContractDAO.cs
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DAO
{
    /// <summary>
    /// Data access for SupplierContract.
    /// </summary>
    public class SupplierContractDAO : StockContext, IDAO<SupplierContract, SupplierContractDetailDTO>
    {
        /// <inheritdoc/>
        public bool Insert(SupplierContract entity)
        {
            try
            {
                // Ensure audit fields are populated before insert
                var now = DateTime.UtcNow;
                entity.CreatedDate = now;
                entity.ModifiedDate = now;

                DbContext.SupplierContract.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var messages = ex.EntityValidationErrors
                                     .SelectMany(e => e.ValidationErrors)
                                     .Select(v => $"{v.PropertyName}: {v.ErrorMessage}");
                throw new Exception(
                    $"Failed to insert supplier contract. Validation errors:\n{string.Join("\n", messages)}",
                    ex
                );
            }
            catch (Exception ex)
            {
                // Unwrap to root cause
                var root = ex;
                while (root.InnerException != null)
                    root = root.InnerException;

                throw new Exception(
                    $"An unexpected error occurred while inserting supplier contract. Root Cause: {root.Message}",
                    ex
                );
            }
        }

        /// <inheritdoc/>
        public bool Update(SupplierContract entity)
        {
            try
            {
                var existing = DbContext.SupplierContract.Find(entity.ContractID);
                if (existing == null) return false;

                existing.StartDate = entity.StartDate;
                existing.EndDate = entity.EndDate;
                existing.RenewalDate = entity.RenewalDate;
                existing.Terms = entity.Terms;
                existing.ModifiedDate = DateTime.UtcNow; // update timestamp

                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var messages = ex.EntityValidationErrors
                                     .SelectMany(e => e.ValidationErrors)
                                     .Select(v => $"{v.PropertyName}: {v.ErrorMessage}");
                throw new Exception(
                    $"Failed to update supplier contract. Validation errors:\n{string.Join("\n", messages)}",
                    ex
                );
            }
            catch (Exception ex)
            {
                // Unwrap to root cause
                var root = ex;
                while (root.InnerException != null)
                    root = root.InnerException;

                throw new Exception(
                    $"An unexpected error occurred while updating supplier contract. Root Cause: {root.Message}",
                    ex
                );
            }
        }

        /// <inheritdoc/>
        public bool Delete(SupplierContract entity)
        {
            try
            {
                var existing = DbContext.SupplierContract.Find(entity.ContractID);
                if (existing == null) return false;

                DbContext.SupplierContract.Remove(existing);
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                var root = ex;
                while (root.InnerException != null)
                    root = root.InnerException;

                throw new Exception(
                    $"An unexpected error occurred while deleting supplier contract. Root Cause: {root.Message}",
                    ex
                );
            }
        }

        /// <inheritdoc/>
        public bool GetBack(int id) => throw new NotSupportedException("Undelete not supported for supplier contracts.");

        /// <inheritdoc/>
        public List<SupplierContractDetailDTO> Select()
        {
            try
            {
                // Corrected: Use DbContext.SUPPLIER for the join, matching your DbSet name
                // Also, ensure the join key and the selected name property are correct for your SUPPLIER entity
                return DbContext.SupplierContract
                    .Join(DbContext.SUPPLIER, // Changed from DbContext.Supplier to DbContext.SUPPLIER
                          contract => contract.SupplierID,
                          supplier => supplier.ID, // Assuming 'ID' is the primary key in your SUPPLIER entity
                          (contract, supplier) => new SupplierContractDetailDTO
                          {
                              ContractID = contract.ContractID,
                              SupplierID = contract.SupplierID,
                              SupplierName = supplier.SupplierName, // Assuming 'SupplierName' is the property holding the supplier name in your SUPPLIER entity
                              ContractNumber = contract.ContractNumber,
                              StartDate = contract.StartDate,
                              EndDate = contract.EndDate,
                              RenewalDate = contract.RenewalDate,
                              Terms = contract.Terms
                          })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve supplier contracts. {ex.Message}", ex);
            }
        }
    }
}