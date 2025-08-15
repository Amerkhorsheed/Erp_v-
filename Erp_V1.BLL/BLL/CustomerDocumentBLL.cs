// File: CustomerDocumentBLL.cs
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using System.Threading.Tasks;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    public class CustomerDocumentBLL
    {
        private readonly CustomerDocumentDAO _dao = new CustomerDocumentDAO();

        public async Task<bool> InsertAsync(CustomerDocumentDTO dto)
        {
            var entity = new CUSTOMERDOCUMENT
            {
                CustomerID = dto.CustomerID,
                FileName = dto.FileName,
                FilePath = dto.FilePath,
                UploadDate = DateTime.Now
            };
            return await _dao.InsertAsync(entity);
        }

        public async Task<bool> UpdateAsync(CustomerDocumentDTO dto)
        {
            var entity = new CUSTOMERDOCUMENT
            {
                ID = dto.ID,
                FileName = dto.FileName,
                FilePath = dto.FilePath,
                // keep original UploadDate
            };
            return await _dao.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(CustomerDocumentDTO dto)
            => await _dao.DeleteAsync(new CUSTOMERDOCUMENT { ID = dto.ID });

        public CustomerDocumentListDTO Select()
            => new CustomerDocumentListDTO { Documents = _dao.Select() };
    }
}
