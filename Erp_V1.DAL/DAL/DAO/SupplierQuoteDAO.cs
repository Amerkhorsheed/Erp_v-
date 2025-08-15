using Erp_V1.DAL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Erp_V1.DAL.DAL;


namespace Erp_V1.DAL.DAO
{
    public class SupplierQuoteDAO : StockContext,
        IDAO<SupplierQuote, SupplierQuoteDetailDTO>
    {
        public bool Insert(SupplierQuote entity)
        {
            entity.CreatedDate = DateTime.Now;
            DbContext.SupplierQuote.Add(entity);
            return DbContext.SaveChanges() > 0;
        }

        public bool Update(SupplierQuote entity)
        {
            var e = DbContext.SupplierQuote.Find(entity.QuoteID);
            if (e == null) return false;
            // Assuming SupplierID and RequestID should not be updated directly through this form's update,
            // or they are handled separately. If they need to be updated, add them here.
            e.QuoteDate = entity.QuoteDate;
            e.TotalAmount = entity.TotalAmount;
            e.Currency = entity.Currency;
            e.Details = entity.Details;
            return DbContext.SaveChanges() > 0;
        }

        public bool Delete(SupplierQuote entity)
        {
            var e = DbContext.SupplierQuote.Find(entity.QuoteID);
            if (e == null) return false;
            DbContext.SupplierQuote.Remove(e);
            return DbContext.SaveChanges() > 0;
        }

        public bool GetBack(int id)
            => throw new NotSupportedException("Undelete not supported for quotes.");

        public List<SupplierQuoteDetailDTO> Select()
        {
            // Perform a JOIN with the SUPPLIER table to get the SupplierName
            return DbContext.SupplierQuote
                .Join(DbContext.SUPPLIER, // Assuming your Supplier DbSet is named 'SUPPLIER'
                      quote => quote.SupplierID,
                      supplier => supplier.ID, // Assuming 'ID' is the primary key in your SUPPLIER entity
                      (quote, supplier) => new SupplierQuoteDetailDTO
                      {
                          QuoteID = quote.QuoteID,
                          RequestID = quote.RequestID,
                          SupplierID = quote.SupplierID,
                          SupplierName = supplier.SupplierName, // Populate the SupplierName
                          QuoteDate = quote.QuoteDate,
                          TotalAmount = quote.TotalAmount,
                          Currency = quote.Currency,
                          Details = quote.Details
                      })
                .ToList();
        }
    }
}