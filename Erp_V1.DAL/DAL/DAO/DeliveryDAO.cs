using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Erp_V1.DAL.DAO
{
    public class DeliveryDAO : StockContext, IDAO<DELIVERY, DeliveryDetailDTO>
    {
        public bool Delete(DELIVERY entity)
        {
            try
            {
                var delivery = DbContext.DELIVERY.First(x => x.ID == entity.ID);
                delivery.isDeleted = true;
                delivery.DeletedDate = DateTime.Now;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Delivery deletion failed", ex);
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                var delivery = DbContext.DELIVERY.First(x => x.ID == ID);
                delivery.isDeleted = false;
                delivery.DeletedDate = null;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Delivery restoration failed", ex);
            }
        }

        public bool Insert(DELIVERY entity)
        {
            try
            {
                DbContext.DELIVERY.Add(entity);
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Delivery insertion failed", ex);
            }
        }

        public List<DeliveryDetailDTO> Select()
        {
            return ExecuteDeliveryQuery(false);
        }

        public List<DeliveryDetailDTO> Select(bool isDeleted)
        {
            return ExecuteDeliveryQuery(isDeleted);
        }

        public bool Update(DELIVERY entity)
        {
            try
            {
                var delivery = DbContext.DELIVERY.First(x => x.ID == entity.ID);
                delivery.DeliveryPersonID = entity.DeliveryPersonID;
                delivery.Status = entity.Status;
                delivery.AssignedDate = entity.AssignedDate;
                delivery.DeliveredDate = entity.DeliveredDate;
                return DbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Delivery update failed", ex);
            }
        }

        private List<DeliveryDetailDTO> ExecuteDeliveryQuery(bool isDeleted)
        {
            try
            {
                var query = from d in DbContext.DELIVERY.Where(x => x.isDeleted == isDeleted)
                            join s in DbContext.SALES on d.SalesID equals s.ID
                            join c in DbContext.CUSTOMER on s.CustomerID equals c.ID
                            join e in DbContext.EMPLOYEE on d.DeliveryPersonID equals e.ID into de
                            from deliveryPerson in de.DefaultIfEmpty()
                            select new DeliveryDetailDTO
                            {
                                DeliveryID = d.ID,
                                SalesID = d.SalesID,
                                DeliveryPersonID = d.DeliveryPersonID,
                                DeliveryPersonName = deliveryPerson != null ? deliveryPerson.Name : "Unassigned",
                                CustomerName = c.CustomerName,
                                Address = c.Cust_Address, // Assuming Customer table has an Address field
                                Status = d.Status,
                                AssignedDate = d.AssignedDate,
                                DeliveredDate = d.DeliveredDate
                            };

                return query.OrderByDescending(x => x.AssignedDate).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Delivery query failed", ex);
            }
        }
    }
}