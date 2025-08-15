using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DAO
{
    public class CustomerDocumentDAO : StockContext
    {
        public async Task<bool> InsertAsync(CUSTOMERDOCUMENT e)
        {
            DbContext.CUSTOMERDOCUMENT.Add(e);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(CUSTOMERDOCUMENT e)
        {
            var db = await DbContext.CUSTOMERDOCUMENT.FirstAsync(x => x.ID == e.ID);
            db.FileName = e.FileName;
            db.FilePath = e.FilePath;
            // Assuming UploadDate is not updated during an edit operation
            // db.UploadDate = e.UploadDate; // Uncomment if UploadDate should be updated
            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(CUSTOMERDOCUMENT e)
        {
            var db = await DbContext.CUSTOMERDOCUMENT.FirstAsync(x => x.ID == e.ID);
            DbContext.CUSTOMERDOCUMENT.Remove(db);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public List<CustomerDocumentDTO> Select()
        {
            // Join with the CUSTOMER table to get the CustomerName
            return DbContext.CUSTOMERDOCUMENT
                .Join(DbContext.CUSTOMER, // Join with the CUSTOMER table
                      doc => doc.CustomerID, // Key from CUSTOMERDOCUMENT
                      cust => cust.ID, // Key from CUSTOMER
                      (doc, cust) => new { Document = doc, Customer = cust }) // Result selector
                .Select(x => new CustomerDocumentDTO
                {
                    ID = x.Document.ID,
                    CustomerID = x.Document.CustomerID,
                    CustomerName = x.Customer.CustomerName, // Map CustomerName from the joined CUSTOMER entity
                    FileName = x.Document.FileName,
                    FilePath = x.Document.FilePath,
                    UploadDate = x.Document.UploadDate
                })
                .ToList();
        }
    }
}
