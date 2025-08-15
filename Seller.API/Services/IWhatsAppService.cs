using Erp_V1.DAL.DAL;
using System.Collections.Generic;

namespace Seller.API.Services
{
    public interface IWhatsAppService
    {
        string GenerateInvoiceClickToChatUrl(CUSTOMER customer, List<SALES> sales, string invoiceUrl);
    }
}