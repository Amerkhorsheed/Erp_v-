using Erp_V1.DAL.DTO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DAO
{
    public class CustomerInteractionDAO : StockContext
    {
        public async Task<bool> InsertAsync(CUSTOMERINTERACTION e)
        {
            DbContext.CUSTOMERINTERACTION.Add(e);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(CUSTOMERINTERACTION e)
        {
            var db = await DbContext.CUSTOMERINTERACTION.FirstAsync(x => x.ID == e.ID);
            db.Type = e.Type;
            db.Notes = e.Notes;
            // Assuming InteractionDate is not updated during an edit operation
            // db.InteractionDate = e.InteractionDate; // Uncomment if InteractionDate should be updated
            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(CUSTOMERINTERACTION e)
        {
            var db = await DbContext.CUSTOMERINTERACTION.FirstAsync(x => x.ID == e.ID);
            DbContext.CUSTOMERINTERACTION.Remove(db);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public List<CustomerInteractionDTO> Select()
        {
            // Join with the CUSTOMER table to get the CustomerName
            return DbContext.CUSTOMERINTERACTION
                .Join(DbContext.CUSTOMER, // Join with the CUSTOMER table
                      interaction => interaction.CustomerID, // Key from CUSTOMERINTERACTION
                      customer => customer.ID, // Key from CUSTOMER
                      (interaction, customer) => new { Interaction = interaction, Customer = customer }) // Result selector
                .Select(x => new CustomerInteractionDTO
                {
                    ID = x.Interaction.ID,
                    CustomerID = x.Interaction.CustomerID,
                    CustomerName = x.Customer.CustomerName, // Map CustomerName from the joined CUSTOMER entity
                    Type = x.Interaction.Type,
                    Notes = x.Interaction.Notes,
                    InteractionDate = x.Interaction.InteractionDate
                })
                .ToList();
        }
    }
}
