using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using System.Threading.Tasks;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    public class CustomerCampaignBLL
    {
        private readonly CustomerCampaignDAO _dao = new CustomerCampaignDAO();

        public async Task<bool> InsertAsync(CustomerCampaignDTO dto)
        {
            var now = DateTime.Now;  // or .UtcNow if you prefer UTC timestamps

            var entity = new CUSTOMERCAMPAIGN
            {
                CustomerID = dto.CustomerID,
                CampaignName = dto.CampaignName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Impact = dto.Impact,
                CreatedDate = now       // ← ensure a valid, in‑range value
            };

            return await _dao.InsertAsync(entity);
        }

        public async Task<bool> UpdateAsync(CustomerCampaignDTO dto)
        {
            var entity = new CUSTOMERCAMPAIGN
            {
                ID = dto.ID,
                CustomerID = dto.CustomerID,
                CampaignName = dto.CampaignName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Impact = dto.Impact
                // notice: we don’t set CreatedDate here
            };
            return await _dao.UpdateAsync(entity);
        }


        public async Task<bool> DeleteAsync(CustomerCampaignDTO dto)
        {
            var entity = new CUSTOMERCAMPAIGN { ID = dto.ID };
            return await _dao.DeleteAsync(entity);
        }

        public CustomerCampaignListDTO Select()
        {
            return new CustomerCampaignListDTO { Campaigns = _dao.Select() };
        }
    }
}
