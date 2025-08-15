using Erp_V1.DAL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using Erp_V1.DAL.DAL;


namespace Erp_V1.DAL.DAO
{

    public class SupplyScheduleDAO : StockContext, IDAO<SupplySchedule, SupplyScheduleDetailDTO>
    {
        private DbSet<SupplySchedule> Schedules => DbContext.Set<SupplySchedule>();

        public bool Insert(SupplySchedule entity)
        {
            try
            {
                entity.CreatedDate = DateTime.UtcNow;
                Schedules.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (DbEntityValidationException vex)
            {
                var msgs = vex.EntityValidationErrors
                              .SelectMany(ev => ev.ValidationErrors)
                              .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception("Insert validation failed:\n" + string.Join("\n", msgs), vex);
            }
        }

        public bool Update(SupplySchedule entity)
        {
            try
            {
                // attach & mark only the properties we allow
                var dbEntry = Schedules.Find(entity.ScheduleID);
                if (dbEntry == null)
                    throw new KeyNotFoundException($"ScheduleID {entity.ScheduleID} not found.");

                DbContext.Entry(dbEntry).CurrentValues.SetValues(entity);
                dbEntry.CreatedDate = dbEntry.CreatedDate; // preserve
                dbEntry.ModifiedDate = DateTime.UtcNow;

                // concurrency
                DbContext.Entry(dbEntry).OriginalValues["RowVersion"] = entity.RowVersion;
                return DbContext.SaveChanges() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Concurrency failure: this schedule has been modified elsewhere.");
            }
            catch (DbEntityValidationException vex)
            {
                var msgs = vex.EntityValidationErrors
                              .SelectMany(ev => ev.ValidationErrors)
                              .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new Exception("Update validation failed:\n" + string.Join("\n", msgs), vex);
            }
        }

        public bool Delete(SupplySchedule entity)
        {
            var dbEntry = Schedules.Find(entity.ScheduleID);
            if (dbEntry == null) return false;
            Schedules.Remove(dbEntry);
            return DbContext.SaveChanges() > 0;
        }

        public bool GetBack(int id)
            => throw new NotSupportedException("Undelete not supported.");

        public List<SupplyScheduleDetailDTO> Select()
        {
            return Schedules
                .AsNoTracking()
                // Use the correct property name: SUPPLIER
                .Include(s => s.SupplierContract.SUPPLIER) // Corrected from Supplier to SUPPLIER
                .Select(s => new SupplyScheduleDetailDTO
                {
                    ScheduleID = s.ScheduleID,
                    ContractID = s.ContractID,
                    // Use the correct property path
                    SupplierID = s.SupplierContract.SUPPLIER.ID, // Corrected from Supplier to SUPPLIER
                    SupplierName = s.SupplierContract.SUPPLIER.SupplierName, // Corrected from Supplier to SUPPLIER
                    ExpectedDate = s.ExpectedDate,
                    Quantity = s.Quantity,
                    Status = s.Status,
                    RowVersion = s.RowVersion
                })
                .OrderBy(s => s.ExpectedDate)
                .ThenBy(s => s.SupplierName)
                .ToList();
        }
    }


}
