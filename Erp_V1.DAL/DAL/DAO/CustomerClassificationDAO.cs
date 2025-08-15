using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Erp_V1.DAL.DAO
{
    public class CustomerClassificationDAO : StockContext
    {
        public async Task<bool> InsertAsync(CUSTOMERCLASSIFICATION e)
        {
            DbContext.CUSTOMERCLASSIFICATION.Add(e);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(CUSTOMERCLASSIFICATION e)
        {
            var db = await DbContext.CUSTOMERCLASSIFICATION.FirstAsync(x => x.ID == e.ID);
            db.Tier = e.Tier;
            // Assuming AssignedDate is not updated during an edit operation
            // db.AssignedDate = e.AssignedDate; // Uncomment if AssignedDate should be updated
            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(CUSTOMERCLASSIFICATION e)
        {
            var db = await DbContext.CUSTOMERCLASSIFICATION.FirstAsync(x => x.ID == e.ID);
            DbContext.CUSTOMERCLASSIFICATION.Remove(db);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public List<CustomerClassificationDTO> Select()
        {
            // Join with the CUSTOMER table to get the CustomerName
            return DbContext.CUSTOMERCLASSIFICATION
                .Join(DbContext.CUSTOMER, // Join with the CUSTOMER table
                      classification => classification.CustomerID, // Key from CUSTOMERCLASSIFICATION
                      customer => customer.ID, // Key from CUSTOMER
                      (classification, customer) => new { Classification = classification, Customer = customer }) // Result selector
                .Select(x => new CustomerClassificationDTO
                {
                    ID = x.Classification.ID,
                    CustomerID = x.Classification.CustomerID,
                    CustomerName = x.Customer.CustomerName, // Map CustomerName from the joined CUSTOMER entity
                    Tier = x.Classification.Tier,
                    AssignedDate = x.Classification.AssignedDate
                }).ToList();
        }
    }
}
