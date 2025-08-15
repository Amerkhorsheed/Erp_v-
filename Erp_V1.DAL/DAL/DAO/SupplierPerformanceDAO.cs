using Erp_V1.DAL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Erp_V1.DAL.DAL;

using System.Text; // Keep this as it's often used with String methods
using System.Threading.Tasks; // Keep this as it's often used with async/await (even if not directly here)

namespace Erp_V1.DAL.DAO
{
    public class SupplierPerformanceDAO : StockContext,
        IDAO<SupplierPerformance, SupplierPerformanceDetailDTO>
    {
        public bool Insert(SupplierPerformance entity)
        {
            entity.CreatedDate = DateTime.Now;
            DbContext.SupplierPerformance.Add(entity);
            return DbContext.SaveChanges() > 0;
        }

        public bool Update(SupplierPerformance entity)
        {
            var e = DbContext.SupplierPerformance.Find(entity.PerformanceID);
            if (e == null) return false;

            // Ensure SupplierID is updated as well during an update operation
            e.SupplierID = entity.SupplierID; // Added this line
            e.EvaluationDate = entity.EvaluationDate;
            e.Score = entity.Score;
            e.ParameterDetails = entity.ParameterDetails;
            e.Comments = entity.Comments;
            return DbContext.SaveChanges() > 0;
        }

        public bool Delete(SupplierPerformance entity)
        {
            var e = DbContext.SupplierPerformance.Find(entity.PerformanceID);
            if (e == null) return false;
            DbContext.SupplierPerformance.Remove(e);
            return DbContext.SaveChanges() > 0;
        }

        public bool GetBack(int id)
            => throw new NotSupportedException("Undelete not supported for performance.");

        public List<SupplierPerformanceDetailDTO> Select()
        {
            // Corrected: Join with SUPPLIER table to include SupplierName
            return DbContext.SupplierPerformance
                .Join(DbContext.SUPPLIER, // Join with your Supplier DbSet
                      performance => performance.SupplierID,
                      supplier => supplier.ID, // Assuming 'ID' is the primary key in your SUPPLIER entity
                      (performance, supplier) => new SupplierPerformanceDetailDTO
                      {
                          PerformanceID = performance.PerformanceID,
                          SupplierID = performance.SupplierID,
                          SupplierName = supplier.SupplierName, // Populate SupplierName from the joined Supplier entity
                          EvaluationDate = performance.EvaluationDate,
                          Score = performance.Score,
                          ParameterDetails = performance.ParameterDetails,
                          Comments = performance.Comments
                      })
                .ToList();
        }
    }
}