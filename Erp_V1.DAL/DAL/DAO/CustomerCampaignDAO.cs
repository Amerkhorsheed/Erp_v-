using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Erp_V1.DAL.DAO
{
    public class CustomerCampaignDAO : StockContext
    {
        public async Task<bool> InsertAsync(CUSTOMERCAMPAIGN entity)
        {
            DbContext.CUSTOMERCAMPAIGN.Add(entity);
            return await DbContext.SaveChangesAsync() > 0;
        }

        // Improved: This method is now more performant.
        // It attaches the entity and sets its state to modified,
        // avoiding an extra database call to fetch the original record.
        public async Task<bool> UpdateAsync(CUSTOMERCAMPAIGN entity)
        {
            // 1) Load the existing record (including its CreatedDate)
            var dbEntity = await DbContext.CUSTOMERCAMPAIGN.FindAsync(entity.ID);
            if (dbEntity == null) return false;

            // 2) Copy over only the fields you want to update:
            dbEntity.CustomerID = entity.CustomerID;
            dbEntity.CampaignName = entity.CampaignName;
            dbEntity.StartDate = entity.StartDate;
            dbEntity.EndDate = entity.EndDate;
            dbEntity.Impact = entity.Impact;
            // ← leave dbEntity.CreatedDate untouched!

            // 3) Persist:
            return await DbContext.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteAsync(CUSTOMERCAMPAIGN entity)
        {
            // For deletion, fetching first is safer to ensure the record exists.
            var dbEntity = await DbContext.CUSTOMERCAMPAIGN.FirstAsync(x => x.ID == entity.ID);
            DbContext.CUSTOMERCAMPAIGN.Remove(dbEntity);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public List<CustomerCampaignDTO> Select()
        {
            return DbContext.CUSTOMERCAMPAIGN
                .Join(DbContext.CUSTOMER,
                      campaign => campaign.CustomerID,
                      customer => customer.ID,
                      (campaign, customer) => new { Campaign = campaign, Customer = customer })
                .Select(x => new CustomerCampaignDTO
                {
                    ID = x.Campaign.ID,
                    CustomerID = x.Campaign.CustomerID,
                    CustomerName = x.Customer.CustomerName,
                    CampaignName = x.Campaign.CampaignName,
                    StartDate = x.Campaign.StartDate,
                    EndDate = x.Campaign.EndDate,
                    Impact = x.Campaign.Impact
                })
                .ToList();
        }
    }
}
