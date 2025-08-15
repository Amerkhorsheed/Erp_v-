using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    // SupplyScheduleBLL.cs
    public class SupplyScheduleBLL : IBLL<SupplyScheduleDetailDTO, SupplyScheduleDTO>
    {
        private readonly SupplyScheduleDAO _dao = new SupplyScheduleDAO();

        public SupplyScheduleDTO Select()
            => new SupplyScheduleDTO { Schedules = _dao.Select() };

        public bool Insert(SupplyScheduleDetailDTO dto)
        {
            Validate(dto, isNew: true);
            var entity = MapToEntity(dto, isNew: true);
            return _dao.Insert(entity);
        }

        public bool Update(SupplyScheduleDetailDTO dto)
        {
            Validate(dto, isNew: false);
            var entity = MapToEntity(dto, isNew: false);
            return _dao.Update(entity);
        }

        public bool Delete(SupplyScheduleDetailDTO dto)
        {
            if (dto?.ScheduleID <= 0)
                throw new ArgumentException("Valid ScheduleID required to delete.");
            return _dao.Delete(new SupplySchedule { ScheduleID = dto.ScheduleID });
        }

        public bool GetBack(SupplyScheduleDetailDTO dto)
            => throw new NotSupportedException();

        private void Validate(SupplyScheduleDetailDTO dto, bool isNew)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (!isNew && dto.ScheduleID <= 0)
                throw new ArgumentException("Valid ScheduleID required to update.");
            if (dto.Quantity <= 0)
                throw new ArgumentException("Quantity must be > 0.");
            if (dto.ExpectedDate.Date < DateTime.Today)
                throw new ArgumentException("Expected date cannot be in the past.");
            if (string.IsNullOrWhiteSpace(dto.Status))
                throw new ArgumentException("Status is required.");
        }

        private SupplySchedule MapToEntity(SupplyScheduleDetailDTO dto, bool isNew)
        {
            return new SupplySchedule
            {
                ScheduleID = dto.ScheduleID,
                ContractID = dto.ContractID,
                ExpectedDate = dto.ExpectedDate,
                Quantity = dto.Quantity,
                Status = dto.Status.Trim(),
                RowVersion = isNew ? null : dto.RowVersion
            };
        }
    }


}
