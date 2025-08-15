// File: CustomerClassificationBLL.cs
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using System.Threading.Tasks;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    public class CustomerClassificationBLL
    {
        private readonly CustomerClassificationDAO _dao = new CustomerClassificationDAO();

        public async Task<bool> InsertAsync(CustomerClassificationDTO dto)
        {
            var entity = new CUSTOMERCLASSIFICATION
            {
                CustomerID = dto.CustomerID,
                Tier = dto.Tier,
                AssignedDate = dto.AssignedDate == default
                                 ? DateTime.Now
                                 : dto.AssignedDate
            };
            return await _dao.InsertAsync(entity);
        }

        public async Task<bool> UpdateAsync(CustomerClassificationDTO dto)
        {
            var entity = new CUSTOMERCLASSIFICATION
            {
                ID = dto.ID,
                Tier = dto.Tier
                // leave AssignedDate untouched
            };
            return await _dao.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(CustomerClassificationDTO dto)
            => await _dao.DeleteAsync(new CUSTOMERCLASSIFICATION { ID = dto.ID });

        public CustomerClassificationListDTO Select()
            => new CustomerClassificationListDTO { Classifications = _dao.Select() };
    }
}
