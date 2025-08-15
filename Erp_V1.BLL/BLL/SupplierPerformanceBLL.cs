using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System.Linq;
using System.Collections.Generic; // Added for List
using System; // Added for NotSupportedException
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    public class SupplierPerformanceBLL
        : IBLL<SupplierPerformanceDetailDTO, SupplierPerformanceDTO>
    {
        private readonly SupplierPerformanceDAO _dao = new SupplierPerformanceDAO();
        private readonly SupplierDAO _supplierDao = new SupplierDAO(); // New: Instance of SupplierDAO

        public SupplierPerformanceDTO Select()
            => new SupplierPerformanceDTO { Performances = _dao.Select() };

        public bool Insert(SupplierPerformanceDetailDTO dto)
            => _dao.Insert(new SupplierPerformance
            {
                SupplierID = dto.SupplierID, // Ensure SupplierID is correctly mapped
                EvaluationDate = dto.EvaluationDate,
                Score = dto.Score,
                ParameterDetails = dto.ParameterDetails,
                Comments = dto.Comments
            });

        public bool Update(SupplierPerformanceDetailDTO dto)
            => _dao.Update(new SupplierPerformance
            {
                PerformanceID = dto.PerformanceID,
                SupplierID = dto.SupplierID, // Ensure SupplierID is updated in edit mode
                EvaluationDate = dto.EvaluationDate,
                Score = dto.Score,
                ParameterDetails = dto.ParameterDetails,
                Comments = dto.Comments
            });

        public bool Delete(SupplierPerformanceDetailDTO dto)
            => _dao.Delete(new SupplierPerformance { PerformanceID = dto.PerformanceID });

        public bool GetBack(SupplierPerformanceDetailDTO dto)
            => _dao.GetBack(dto.PerformanceID);

        public decimal CalculateAverageScore(int supplierId)
        {
            var scores = _dao.Select()
                                 .Where(x => x.SupplierID == supplierId)
                                 .Select(x => x.Score);
            return scores.Any() ? scores.Average() : 0m;
        }

        /// <summary>
        /// Retrieves a list of all active suppliers for selection.
        /// </summary>
        /// <returns>A list of SupplierDetailDTO objects.</returns>
        public List<SupplierDetailDTO> GetSuppliersForSelection()
        {
            return _supplierDao.Select(isDeleted: false); // Get only non-deleted suppliers
        }
    }
}