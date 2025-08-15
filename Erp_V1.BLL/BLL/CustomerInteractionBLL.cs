// File: CustomerInteractionBLL.cs
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System.Threading.Tasks;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    public class CustomerInteractionBLL
    {
        private readonly CustomerInteractionDAO _dao = new CustomerInteractionDAO();

        public async Task<bool> InsertAsync(CustomerInteractionDTO dto)
        {
            var entity = new CUSTOMERINTERACTION
            {
                CustomerID = dto.CustomerID,
                Type = dto.Type,
                Notes = dto.Notes,
                InteractionDate = dto.InteractionDate
            };
            return await _dao.InsertAsync(entity);
        }

        public async Task<bool> UpdateAsync(CustomerInteractionDTO dto)
        {
            var entity = new CUSTOMERINTERACTION
            {
                ID = dto.ID,
                Type = dto.Type,
                Notes = dto.Notes
                // keep original InteractionDate
            };
            return await _dao.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(CustomerInteractionDTO dto)
            => await _dao.DeleteAsync(new CUSTOMERINTERACTION { ID = dto.ID });

        public CustomerInteractionListDTO Select()
            => new CustomerInteractionListDTO { Interactions = _dao.Select() };
    }
}
