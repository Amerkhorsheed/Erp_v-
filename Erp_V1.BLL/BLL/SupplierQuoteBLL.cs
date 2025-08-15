using Erp_V1.DAL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using System;
using System.Linq;
using Erp_V1.DAL.DAL;

namespace Erp_V1.BLL
{
    public class SupplierQuoteBLL
        : IBLL<SupplierQuoteDetailDTO, SupplierQuoteDTO>
    {
        private readonly SupplierQuoteDAO _dao = new SupplierQuoteDAO();

        public SupplierQuoteDTO Select()
            => new SupplierQuoteDTO { Quotes = _dao.Select() };

        public bool Insert(SupplierQuoteDetailDTO dto)
            => _dao.Insert(new SupplierQuote
            {
                RequestID = dto.RequestID,
                SupplierID = dto.SupplierID,
                QuoteDate = dto.QuoteDate,
                TotalAmount = dto.TotalAmount,
                Currency = dto.Currency,
                Details = dto.Details
            });

        public bool Update(SupplierQuoteDetailDTO dto)
            => _dao.Update(new SupplierQuote
            {
                QuoteID = dto.QuoteID,
                QuoteDate = dto.QuoteDate,
                TotalAmount = dto.TotalAmount,
                Currency = dto.Currency,
                Details = dto.Details
            });

        public bool Delete(SupplierQuoteDetailDTO dto)
            => _dao.Delete(new SupplierQuote { QuoteID = dto.QuoteID });

        public bool GetBack(SupplierQuoteDetailDTO dto)
            => _dao.GetBack(dto.QuoteID);

        public SupplierQuoteDTO Compare(Guid requestId)
        {
            var list = _dao.Select()
                           .Where(q => q.RequestID == requestId)
                           .OrderBy(q => q.TotalAmount)
                           .ToList();
            return new SupplierQuoteDTO { Quotes = list };
        }
    }
}
