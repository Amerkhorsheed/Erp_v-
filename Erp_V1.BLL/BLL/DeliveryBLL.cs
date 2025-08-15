using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    public class DeliveryBLL : IBLL<DeliveryDetailDTO, DeliveryDTO>
    {
        private readonly DeliveryDAO _dao = new DeliveryDAO();

        public bool Delete(DeliveryDetailDTO entity)
        {
            return _dao.Delete(new DELIVERY { ID = entity.DeliveryID });
        }

        public bool GetBack(DeliveryDetailDTO entity)
        {
            return _dao.GetBack(entity.DeliveryID);
        }

        public bool Insert(DeliveryDetailDTO entity)
        {
            var delivery = new DELIVERY
            {
                SalesID = entity.SalesID,
                Status = "Pending"
            };
            return _dao.Insert(delivery);
        }

        public DeliveryDTO Select()
        {
            return new DeliveryDTO { Deliveries = _dao.Select() };
        }

        public bool Update(DeliveryDetailDTO entity)
        {
            var delivery = new DELIVERY
            {
                ID = entity.DeliveryID,
                DeliveryPersonID = entity.DeliveryPersonID,
                Status = entity.Status,
                AssignedDate = entity.AssignedDate,
                DeliveredDate = entity.DeliveredDate
            };
            return _dao.Update(delivery);
        }
    }
}