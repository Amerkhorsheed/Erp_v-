using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Erp_V1.DAL.DAL;

namespace Erp_V1.DAL.DAO
{
    public class CustomerPointsDAO : StockContext
    {
        public async Task<bool> InsertAsync(CUSTOMERPOINTS e)
        {
            DbContext.CUSTOMERPOINTS.Add(e);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(CUSTOMERPOINTS e)
        {
            var db = await DbContext.CUSTOMERPOINTS.FirstAsync(x => x.ID == e.ID);
            db.Points = e.Points;
            db.LastUpdated = e.LastUpdated;
            return await DbContext.SaveChangesAsync() > 0;
        }

        public List<CustomerPointsDTO> Select()
        {
            // Join with the CUSTOMER table to get the CustomerName
            return DbContext.CUSTOMERPOINTS
                .Join(DbContext.CUSTOMER, // Join with the CUSTOMER table
                      points => points.CustomerID, // Key from CUSTOMERPOINTS
                      customer => customer.ID, // Key from CUSTOMER
                      (points, customer) => new { PointsEntry = points, Customer = customer }) // Result selector
                .Select(x => new CustomerPointsDTO
                {
                    ID = x.PointsEntry.ID,
                    CustomerID = x.PointsEntry.CustomerID,
                    CustomerName = x.Customer.CustomerName, // Map CustomerName from the joined CUSTOMER entity
                    Points = x.PointsEntry.Points,
                    LastUpdated = x.PointsEntry.LastUpdated
                })
                .ToList();
        }
    }
}
