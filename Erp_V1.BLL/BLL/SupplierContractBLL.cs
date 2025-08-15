// Erp_V1.BLL/SupplierContractBLL.cs
using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Business logic for managing supplier contracts.
    /// </summary>
    public class SupplierContractBLL : IBLL<SupplierContractDetailDTO, SupplierContractDTO>
    {
        private readonly SupplierContractDAO _dao = new SupplierContractDAO();

        /// <inheritdoc/>
        public SupplierContractDTO Select()
            => new SupplierContractDTO { Contracts = _dao.Select() };

        /// <inheritdoc/>
        public bool Insert(SupplierContractDetailDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Contract data must be provided.");

            var entity = new SupplierContract
            {
                SupplierID = dto.SupplierID,
                ContractNumber = dto.ContractNumber?.Trim() ?? throw new ArgumentException("ContractNumber is required."),
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                RenewalDate = dto.RenewalDate,
                Terms = dto.Terms?.Trim()
            };

            return _dao.Insert(entity);
        }

        /// <inheritdoc/>
        public bool Update(SupplierContractDetailDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Contract data must be provided.");

            var entity = new SupplierContract
            {
                ContractID = dto.ContractID,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                RenewalDate = dto.RenewalDate,
                Terms = dto.Terms?.Trim()
            };

            return _dao.Update(entity);
        }

        /// <inheritdoc/>
        public bool Delete(SupplierContractDetailDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Contract identifier must be provided.");

            return _dao.Delete(new SupplierContract { ContractID = dto.ContractID });
        }

        /// <inheritdoc/>
        public bool GetBack(SupplierContractDetailDTO dto)
            => throw new NotSupportedException("Undelete not supported for supplier contracts.");
    }
}
