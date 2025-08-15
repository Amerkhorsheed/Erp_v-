using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Erp_V1.DAL;
using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DAO
{
    /// <summary>
    /// Data-access for SupplierCommunication entities.
    /// Implements standard CRUD via IDAO&lt;SupplierCommunication,SupplierCommunicationDetailDTO&gt;.
    /// </summary>
    public class SupplierCommunicationDAO : StockContext,
        IDAO<SupplierCommunication, SupplierCommunicationDetailDTO>
    {
        /// <inheritdoc/>
        public bool Insert(SupplierCommunication entity)
        {
            try
            {
                DbContext.SupplierCommunication.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var messages = ex.EntityValidationErrors
                                     .SelectMany(e => e.ValidationErrors)
                                     .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Failed to insert communication. Validation errors:\n{string.Join("\n", messages)}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert communication.", ex);
            }
        }

        /// <inheritdoc/>
        public bool Update(SupplierCommunication entity)
        {
            try
            {
                var existing = DbContext.SupplierCommunication.Find(entity.CommunicationID);
                if (existing == null)
                    return false;

                // Ensure supplier foreign key is updated as well
                existing.SupplierID = entity.SupplierID;
                existing.CommunicationDate = entity.CommunicationDate;
                existing.Type = entity.Type;
                existing.Subject = entity.Subject;
                existing.Content = entity.Content;
                existing.ReferenceLink = entity.ReferenceLink;

                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var messages = ex.EntityValidationErrors
                                     .SelectMany(e => e.ValidationErrors)
                                     .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception($"Failed to update communication. Validation errors:\n{string.Join("\n", messages)}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update communication.", ex);
            }
        }

        /// <inheritdoc/>
        public bool Delete(SupplierCommunication entity)
        {
            try
            {
                var existing = DbContext.SupplierCommunication.Find(entity.CommunicationID);
                if (existing == null)
                    return false;

                DbContext.SupplierCommunication.Remove(existing);
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete communication.", ex);
            }
        }

        /// <inheritdoc/>
        public bool GetBack(int id)
            => throw new NotSupportedException("Undelete not supported for communications.");

        /// <inheritdoc/>
        public List<SupplierCommunicationDetailDTO> Select()
        {
            try
            {
                // Join with the SUPPLIER table to get the SupplierName
                // Assuming DbContext.SUPPLIER is the DbSet for your Supplier entity
                return DbContext.SupplierCommunication
                    .Join(DbContext.SUPPLIER, // Corrected: Use DbContext.SUPPLIER
                          comm => comm.SupplierID,
                          supp => supp.ID, // Corrected: Assuming 'ID' is the primary key in your SUPPLIER entity
                          (comm, supp) => new SupplierCommunicationDetailDTO
                          {
                              CommunicationID = comm.CommunicationID,
                              SupplierID = comm.SupplierID,
                              SupplierName = supp.SupplierName, // Map SupplierName
                              CommunicationDate = comm.CommunicationDate,
                              Type = comm.Type,
                              Subject = comm.Subject,
                              Content = comm.Content,
                              ReferenceLink = comm.ReferenceLink
                          })
                    .OrderByDescending(c => c.CommunicationDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve communications.", ex);
            }
        }
    }
}