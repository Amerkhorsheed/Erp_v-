// File: CustomerContractListDTO.cs
using Erp_V1.DAL.DTO;
using System.Collections.Generic;

namespace Erp_V1.DAL.DTO
{
    public class CustomerContractListDTO
    {
        public List<CustomerContractDTO> Contracts { get; set; }
    }
}
