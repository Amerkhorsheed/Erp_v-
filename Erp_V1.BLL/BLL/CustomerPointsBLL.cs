// File: CustomerPointsBLL.cs
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using System.Threading.Tasks;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    public class CustomerPointsBLL
    {
        private readonly CustomerPointsDAO _dao = new CustomerPointsDAO();

        public async Task<bool> InsertAsync(CustomerPointsDTO dto)
        {
            var entity = new CUSTOMERPOINTS
            {
                CustomerID = dto.CustomerID,
                Points = dto.Points,
                LastUpdated = DateTime.Now
            };
            return await _dao.InsertAsync(entity);
        }

        public async Task<bool> UpdateAsync(CustomerPointsDTO dto)
        {
            var entity = new CUSTOMERPOINTS
            {
                ID = dto.ID,
                Points = dto.Points,
                LastUpdated = DateTime.Now
            };
            return await _dao.UpdateAsync(entity);
        }

        // Delete not supported
        public Task<bool> DeleteAsync(CustomerPointsDTO dto)
            => Task.FromResult(false);

        public CustomerPointsListDTO Select()
            => new CustomerPointsListDTO { PointsEntries = _dao.Select() };
    }
}
