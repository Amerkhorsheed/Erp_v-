// File: Erp_V1.BLL/SupplierCommunicationBLL.cs
using System.Collections.Generic;
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Business‑logic for supplier communications.
    /// Delegates to SupplierCommunicationDAO which implements IDAO&lt;SupplierCommunication,SupplierCommunicationDetailDTO&gt;.
    /// </summary>
    public class SupplierCommunicationBLL : IBLL<SupplierCommunicationDetailDTO, SupplierCommunicationDTO>
    {
        private readonly SupplierCommunicationDAO _commDao = new SupplierCommunicationDAO();

        /// <summary>
        /// Retrieves all communications.
        /// </summary>
        public SupplierCommunicationDTO Select()
        {
            var list = _commDao.Select();    // <-- use Select(), not SelectAll()
            return new SupplierCommunicationDTO
            {
                Communications = list
            };
        }

        /// <summary>
        /// Inserts a new communication.
        /// </summary>
        public bool Insert(SupplierCommunicationDetailDTO dto)
        {
            var entity = new SupplierCommunication
            {
                SupplierID = dto.SupplierID,
                CommunicationDate = dto.CommunicationDate,
                Type = dto.Type,
                Subject = dto.Subject,
                Content = dto.Content,
                ReferenceLink = dto.ReferenceLink
            };
            return _commDao.Insert(entity);
        }

        /// <summary>
        /// Updates an existing communication.
        /// </summary>
        public bool Update(SupplierCommunicationDetailDTO dto)
        {
            var entity = new SupplierCommunication
            {
                CommunicationID = dto.CommunicationID,
                SupplierID = dto.SupplierID,
                CommunicationDate = dto.CommunicationDate,
                Type = dto.Type,
                Subject = dto.Subject,
                Content = dto.Content,
                ReferenceLink = dto.ReferenceLink
            };
            return _commDao.Update(entity);
        }

        /// <summary>
        /// Deletes a communication by its ID.
        /// </summary>
        public bool Delete(SupplierCommunicationDetailDTO dto)
        {
            // DAO.Delete expects a SupplierCommunication entity
            var entity = new SupplierCommunication
            {
                CommunicationID = dto.CommunicationID
            };
            return _commDao.Delete(entity);
        }

        /// <summary>
        /// Undelete is not supported.
        /// </summary>
        public bool GetBack(SupplierCommunicationDetailDTO dto)
            => throw new System.NotSupportedException("Undelete not supported for communications.");
    }
}
