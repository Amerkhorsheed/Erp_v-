// File: CustomerContractBLL.cs
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using System.Threading.Tasks;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    public class CustomerContractBLL
    {
        private readonly CustomerContractDAO _dao = new CustomerContractDAO();

        // File: CustomerContractBLL.cs
        public async Task<bool> InsertAsync(CustomerContractDTO dto)
        {
            var now = DateTime.Now;
            var entity = new CUSTOMERCONTRACT
            {
                CustomerID = dto.CustomerID,
                ContractNumber = dto.ContractNumber,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status,
                IsDeleted = false,
                CreatedDate = now       
            };

            return await _dao.InsertAsync(entity);
        }


        public async Task<bool> UpdateAsync(CustomerContractDTO dto)
        {
            var entity = new CUSTOMERCONTRACT
            {
                ID = dto.ID,
                CustomerID = dto.CustomerID,
                ContractNumber = dto.ContractNumber,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status
            };
            return await _dao.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(CustomerContractDTO dto)
        {
            return await _dao.DeleteAsync(new CUSTOMERCONTRACT { ID = dto.ID });
        }

        public async Task<bool> GetBackAsync(CustomerContractDTO dto)
        {
            return await _dao.GetBackAsync(dto.ID);
        }

        public async Task<CustomerContractListDTO> SelectAsync()
        {
            var contracts = await _dao.SelectAsync();
            return new CustomerContractListDTO { Contracts = contracts };
        }
    }
}
